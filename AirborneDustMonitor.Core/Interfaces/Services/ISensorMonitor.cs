namespace AirborneDustMonitor.Core.Interfaces.Services
{
    public interface ISensorMonitor
    {
        Task PollOnceAsync();
    }
}
