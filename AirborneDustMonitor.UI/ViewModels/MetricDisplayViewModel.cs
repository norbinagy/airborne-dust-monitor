using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Rules;
using AirborneDustMonitor.Core.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace AirborneDustMonitor.UI.ViewModels
{
    // Ez a ViewModel egy adott metrikához tartozó grafikon és statisztikák megjelenítéséért felelős.
    public partial class MetricDisplayViewModel : ObservableObject, IObserver<MetricData>
    {
        public string Name { get; private set; }
        public MetricType MetricType { get; private set; }
        public PlotModel PlotModel { get; }
        public ObservableCollection<int> AlertingSensors { get; } = new();

        private readonly Dictionary<int, LineSeries> _sensorSeries;
        private readonly AlertService _alertService;
        private IDisposable _unsubscriber;

        [ObservableProperty]
        private decimal _maxValue;
        [ObservableProperty]
        private decimal _minValue;
        [ObservableProperty]
        private decimal _movingAverage;
        [ObservableProperty]
        private decimal _thresholdValue;

        public MetricDisplayViewModel(string name, MetricType metricType, AlertService alertService)
        {
            Name = name;
            MetricType = metricType;
            _sensorSeries = new Dictionary<int, LineSeries>();
            PlotModel = new PlotModel { Title = name };
            _alertService = alertService;

            var legend = new Legend
            {
                LegendPosition = LegendPosition.LeftMiddle,
                LegendPlacement = LegendPlacement.Outside,
                LegendOrientation = LegendOrientation.Vertical,
                LegendBorder = OxyColors.Black,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
            };

            PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            PlotModel.Legends.Add(legend);
            _alertService.AlertRaised += OnAlertRaised;
        }

        public void Subscribe(IObservable<MetricData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
            _alertService.AlertRaised -= OnAlertRaised;
        }

        public void OnNext(MetricData data)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!_sensorSeries.TryGetValue(data.SensorID, out var series))
                {
                    series = new LineSeries { Title = $"Szenzor {data.SensorID}" };
                    _sensorSeries.Add(data.SensorID, series);
                    PlotModel.Series.Add(series);
                }

                var xValue = DateTimeAxis.ToDouble(data.Date);
                series.Points.Add(new DataPoint(xValue, Decimal.ToDouble(data.Value)));

                if (series.Points.Count > 50) series.Points.RemoveAt(0);

                PlotModel.InvalidatePlot(true);

                if (data.Max != MaxValue) MaxValue = data.Max;
                if (data.Min != MinValue) MinValue = data.Min;
                if (data.MovingAverage != MovingAverage) MovingAverage = data.MovingAverage;

            });
        }

        public void OnCompleted()
        {
            _unsubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            Debug.WriteLine($"Hiba a grafikonon ({PlotModel.Title}): {error.Message}");
        }

        partial void OnThresholdValueChanged(decimal value)
        {
            var rule = _alertService.GetRule<ThresholdRule>();
            rule?.SetThreshold(MetricType, value);
        }

        private void OnAlertRaised(Alert Alert)
        {
            if (Alert.MetricType == MetricType && Alert.Type == AlertType.ThresholdExceeded && Alert.Status == AlertStatus.Alerting)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (!AlertingSensors.Contains(Alert.SensorID))
                        AlertingSensors.Add(Alert.SensorID);
                });
            }
            else if (Alert.MetricType == MetricType && Alert.Type == AlertType.ThresholdExceeded && Alert.Status == AlertStatus.Resolved)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (AlertingSensors.Contains(Alert.SensorID))
                        AlertingSensors.Remove(Alert.SensorID);
                });
            }
        }
    }
}
