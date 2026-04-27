using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;

namespace AirborneDustMonitor.Core.Rules
{
    public class BatteryVoltageRule : IAlertRule
    {
        private readonly decimal _minVoltage;
        private readonly decimal _maxVoltage;
        private readonly int _requiredCount;

        private readonly Dictionary<int, int> _counts = new();
        private readonly HashSet<int> _activeAlerts = new();

        public BatteryVoltageRule(IAppSettingsService settings)
        {
            _minVoltage = settings.Current.Alert.BatteryMinVoltage;
            _maxVoltage = settings.Current.Alert.BatteryMaxVoltage;
            _requiredCount = settings.Current.Alert.BatteryVoltageAlertCount;
        }

        public Alert? Evaluate(MetricType metricType, int sensorID, decimal value)
        {
            if (metricType != MetricType.BatteryVoltage)
                return null;

            if (!_counts.ContainsKey(sensorID))
                _counts[sensorID] = 0;

            bool outOfRange = value < _minVoltage || value > _maxVoltage;

            if (outOfRange)
            {
                _counts[sensorID]++;
            }
            else
            {
                _counts[sensorID] = 0;
                _activeAlerts.Remove(sensorID);
            }

            if (_counts[sensorID] >= _requiredCount && !_activeAlerts.Contains(sensorID))
            {
                _activeAlerts.Add(sensorID);
                return new Alert
                (
                    $"Szenzor {sensorID}: Akkumulátor feszültség túl alacsony/magas!",
                    MetricType.BatteryVoltage,
                    sensorID,
                    AlertType.BatteryVoltage,
                    AlertStatus.Alerting
                );
            }

            return null;
        }
    }
}
