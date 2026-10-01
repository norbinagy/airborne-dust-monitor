using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Rules
{
    public interface IAlertRule
    {
        bool NotifyByEmail { get; }
        Alert? Evaluate(MetricType metricType, int sensorID, decimal value);
    }
}
