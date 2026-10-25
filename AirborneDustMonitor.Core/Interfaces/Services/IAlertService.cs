using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Rules;

namespace AirborneDustMonitor.Core.Interfaces.Services
{
    public interface IAlertService
    {
        event Action<Alert>? AlertRaised;
        Task ProcessSampleAsync(MetricType metricType, int sensorID, decimal value);
        T? GetRule<T>() where T : class, IAlertRule;
    }
}
