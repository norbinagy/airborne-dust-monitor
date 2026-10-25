using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Interfaces.Services
{
    public interface IDataService
    {
        IObservable<MetricData> GetStream(MetricType metricType);
        void AddSample(decimal value, MetricType metricType, DateTime date, int sensorID);
    }
}
