using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace AirborneDustMonitor.Core.Services
{
    // Az ISensorDb-t használjuk a szenzoradatok lekérésére, a DataService-t pedig az adatok és statisztikák feldolgozására és tárolására, az AlertService-t pedig a riasztások kiküldésére a nekik átadott szenzoradatok alapján.
    public class SensorPollingService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly DataService _dataService;
        private readonly AlertService _alertService;
        // A _pollingInterval (lekérdezés gyakorisága) és _pollDateIncrement (dátum növelés mértéke) az app demójához kellenek
        private readonly int _pollingInterval;
        private readonly int _pollDateIncrement;

        public SensorPollingService(IServiceScopeFactory serviceScopeFactory, DataService dataService, AlertService alertService, IAppSettingsService appSettingsService)
        {
            this._serviceScopeFactory = serviceScopeFactory;
            this._dataService = dataService;
            this._alertService = alertService;
            this._pollingInterval = appSettingsService.Current.Polling.PollIntervalMilliseconds;
            this._pollDateIncrement = appSettingsService.Current.Polling.PollDateIncrementMinutes;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var currentDate = new DateTime(2024, 03, 28, 10, 13, 00);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ISensorDb>();

                    var sensors = db.GetSensorByDateAsync(currentDate);

                    if (sensors != null && sensors.Result.Count != 0)
                    {
                        foreach (var sensor in sensors.Result)
                        {
                            _dataService.AddSample(sensor.ParticulateMatter, MetricType.ParticulateMatter, sensor.Date, sensor.SensorID);
                            _dataService.AddSample(sensor.Temperature, MetricType.Temperature, sensor.Date, sensor.SensorID);
                            _dataService.AddSample(sensor.Humidity, MetricType.Humidity, sensor.Date, sensor.SensorID);
                            _dataService.AddSample(sensor.BatteryVoltage, MetricType.BatteryVoltage, sensor.Date, sensor.SensorID);

                            _alertService.ProcessSample(MetricType.ParticulateMatter, sensor.SensorID, sensor.ParticulateMatter);
                            _alertService.ProcessSample(MetricType.Temperature, sensor.SensorID, sensor.Temperature);
                            _alertService.ProcessSample(MetricType.Humidity, sensor.SensorID, sensor.Humidity);
                            _alertService.ProcessSample(MetricType.BatteryVoltage, sensor.SensorID, sensor.BatteryVoltage);
                        }
                    }

                    currentDate = currentDate.AddMinutes(_pollDateIncrement);
                    await Task.Delay(_pollingInterval, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // A program leállítása miatt megszakadt a művelet, ezt nem kell külön kezelni
                    return;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Polling hiba (valószínűleg rossz ConnectionString): {ex.Message}");
                    if (stoppingToken.IsCancellationRequested) return;
                    await Task.Delay(10000, CancellationToken.None);
                }
            }
        }
    }
}
