namespace AirborneDustMonitor.Core.Settings
{
    public class AppSettings
    {
        public EmailSettings Email { get; set; } = new EmailSettings();
        public AlertSettings Alert { get; set; } = new AlertSettings();
        public PollingSettings Polling { get; set; } = new PollingSettings();
        public DataSettings Data { get; set; } = new DataSettings();
        public DatabaseSettings Database { get; set; } = new DatabaseSettings();
    }
}
