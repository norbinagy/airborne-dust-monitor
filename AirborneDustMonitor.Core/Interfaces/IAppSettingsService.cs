using AirborneDustMonitor.Core.Settings;
using System.Runtime.Versioning;

namespace AirborneDustMonitor.Core.Interfaces
{
    public interface IAppSettingsService
    {
        AppSettings Current { get; }
        void Save();
    }
}
