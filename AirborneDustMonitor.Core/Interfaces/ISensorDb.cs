using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Interfaces
{
    public interface ISensorDb
    {
        Task<List<Sensor>> GetSensorByDateAsync(DateTime date);
        Task<List<Sensor>> GetSensorAfterDateAsync(DateTime date);
    }
}
