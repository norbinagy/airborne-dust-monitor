using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Interfaces.Rules
{
    public interface IAlertRule
    {
        bool NotifyByEmail { get; }
        Alert? Evaluate(MetricType metricType, int sensorID, decimal value);
    }
}
