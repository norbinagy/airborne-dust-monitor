using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Interfaces.Services
{
    public interface IAlertNotifier
    {
        Task NotifyAsync(Alert alert);
    }
}
