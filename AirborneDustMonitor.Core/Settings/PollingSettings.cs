namespace AirborneDustMonitor.Core.Settings
{
    public class PollingSettings
    {
        private int _pollIntervalMilliseconds = 1000;
        public int PollIntervalMilliseconds
        {
            get => _pollIntervalMilliseconds;
            set => _pollIntervalMilliseconds = Math.Clamp(value, 100, 10000);
        }

        private int _pollDateIncrementMinutes = 1;
        public int PollDateIncrementMinutes
        {
            get => _pollDateIncrementMinutes;
            set => _pollDateIncrementMinutes = Math.Clamp(value, 1, 60);
        }
    }
}
