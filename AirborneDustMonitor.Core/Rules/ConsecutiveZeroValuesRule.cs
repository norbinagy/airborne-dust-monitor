using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;

namespace AirborneDustMonitor.Core.Rules
{
    public class ConsecutiveZeroValuesRule : IAlertRule
    {
        private readonly int _requiredCount;
        private readonly Dictionary<(int sensorID, MetricType metrictype), int> _counts;
        private readonly HashSet<(int sensorID, MetricType metrictype)> _activeAlerts;

        public ConsecutiveZeroValuesRule(IAppSettingsService appSettingsService)
        {
            this._requiredCount = appSettingsService.Current.Alert.ConsecutiveZerosAlertCount;
            this._counts = new Dictionary<(int sensorID, MetricType metrictype), int>();
            this._activeAlerts = new HashSet<(int sensorID, MetricType metrictype)>();
        }

        public Alert? Evaluate(MetricType metricType, int sensorID, decimal value)
        {
            var key = (sensorID, metricType);

            if (!_counts.ContainsKey(key))
            {
                _counts[key] = 0;
            }

            if (value == 0)
            {
                _counts[key]++;
            }
            else
            {
                _counts[key] = 0;
                _activeAlerts.Remove(key);
            }

            if (_counts[key] >= _requiredCount && !_activeAlerts.Contains(key))
            {
                _activeAlerts.Add(key);
                return new Alert
                (
                    $"Szenzor {sensorID}, {metricType} típusú mérése {_counts[key]} egymást követő nulla értéket jelzett.",
                    metricType,
                    sensorID,
                    AlertType.ConsecutiveZeroValues,
                    AlertStatus.Alerting
                );
            }

            return null;
        }
    }
}
