using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Rules
{
    public interface IAlertRule
    {
        Alert? Evaluate(MetricType metricType, int sensorID, decimal value);
    }
}
