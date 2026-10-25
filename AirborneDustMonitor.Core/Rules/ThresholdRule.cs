using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Rules;

namespace AirborneDustMonitor.Core.Rules
{
    public class ThresholdRule : IAlertRule
    {
        private readonly Dictionary<MetricType, decimal> _thresholds;
        private readonly Dictionary<(int, MetricType), AlertStatus> _alertStatuses;
        public bool NotifyByEmail { get; }

        public ThresholdRule()
        {
            _thresholds = new Dictionary<MetricType, decimal>();
            _alertStatuses = new Dictionary<(int, MetricType), AlertStatus>();
            NotifyByEmail = true;
        }

        public void SetThreshold(MetricType metricType, decimal threshold)
        {
            _thresholds[metricType] = threshold;
        }

        public Alert? Evaluate(MetricType metricType, int sensorID, decimal value)
        {
            var key = (sensorID, metricType);
            AlertStatus alertStatus = _alertStatuses.TryGetValue(key, out AlertStatus status) ? status : AlertStatus.Normal;

            if (_thresholds.TryGetValue(metricType, out decimal threshold) && value > threshold && (alertStatus == AlertStatus.Normal || alertStatus == AlertStatus.Resolved))
            {
                _alertStatuses[key] = AlertStatus.Alerting;
                return new Alert
                (
                    $"Figyelem, szenzor {sensorID} ({metricType}, {value}) átlépte a határértéket: {threshold}.",
                    metricType,
                    sensorID,
                    AlertType.ThresholdExceeded,
                    AlertStatus.Alerting
                );
            }
            else if (_thresholds.TryGetValue(metricType, out threshold) && value <= threshold && alertStatus == AlertStatus.Alerting)
            {
                _alertStatuses[key] = AlertStatus.Resolved;
                return new Alert
                (
                    $"Szenzor {sensorID} ({metricType}, {value}) visszatért a normál tartományba (határérték: {threshold}).",
                    metricType,
                    sensorID,
                    AlertType.ThresholdExceeded,
                    AlertStatus.Resolved
                );
            }
            else
            {
                return null;
            }
        }
    }
}
