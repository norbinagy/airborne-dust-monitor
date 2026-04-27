using AirborneDustMonitor.Core.Settings;

namespace AirborneDustMonitor.Core.Interfaces
{
    public interface IAppSettingsService
    {
        AppSettings Current { get; }
        void Save();
    }
}
