using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Rules
{
    public class ThresholdRule : IAlertRule
    {
        private readonly Dictionary<MetricType, decimal> _thresholds;
        private readonly Dictionary<(int, MetricType), AlertStatus> _alertStatuses;
        private readonly Dictionary<MetricType, bool> _emailPending;

        public ThresholdRule()
        {
            _thresholds = new Dictionary<MetricType, decimal>();
            _alertStatuses = new Dictionary<(int, MetricType), AlertStatus>();
            _emailPending = new Dictionary<MetricType, bool>();
        }

        public void SetThreshold(MetricType metricType, decimal threshold)
        {
            _thresholds[metricType] = threshold;
            _emailPending[metricType] = true;
        }

        public bool IsEmailPending(MetricType metricType)
        {
            return _emailPending.TryGetValue(metricType, out bool pending) && pending;
        }

        public void MarkEmailSent(MetricType metricType)
        {
            if (_emailPending.ContainsKey(metricType))
            {
                _emailPending[metricType] = false;
            }
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
