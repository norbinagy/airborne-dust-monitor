using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Persistence;
using AirborneDustMonitor.Core.Interfaces.Services;

namespace AirborneDustMonitor.Core.Services
{
    public class SensorMonitor : ISensorMonitor
    {
        private DateTime _date;
        private readonly IDataService _metrics;
        private readonly IAlertService _alerts;
        private readonly ISensorDb _repo;

        public SensorMonitor(ISensorDb repo, IDataService metrics, IAlertService alerts)
        {
            _repo = repo;
            _metrics = metrics;
            _alerts = alerts;
            _date = new DateTime(2024, 03, 28, 10, 13, 00);
        }

        public async Task PollOnceAsync()
        {
            var rows = await _repo.GetSensorByDateAsync(_date);

            foreach (var row in rows)
            {
                _metrics.AddSample(row.ParticulateMatter, MetricType.ParticulateMatter, row.Date, row.SensorID);
                _metrics.AddSample(row.Temperature, MetricType.Temperature, row.Date, row.SensorID);
                _metrics.AddSample(row.Humidity, MetricType.Humidity, row.Date, row.SensorID);
                _metrics.AddSample(row.BatteryVoltage, MetricType.BatteryVoltage, row.Date, row.SensorID);

                await _alerts.ProcessSampleAsync(MetricType.ParticulateMatter, row.SensorID, row.ParticulateMatter);
                await _alerts.ProcessSampleAsync(MetricType.Temperature, row.SensorID, row.Temperature);
                await _alerts.ProcessSampleAsync(MetricType.Humidity, row.SensorID, row.Humidity);
                await _alerts.ProcessSampleAsync(MetricType.BatteryVoltage, row.SensorID, row.BatteryVoltage);
            }

            _date = _date.AddMinutes(1);
        }
    }
}