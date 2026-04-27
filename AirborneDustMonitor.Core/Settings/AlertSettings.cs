namespace AirborneDustMonitor.Core.Settings
{
    public class AlertSettings
    {
        private int _batteryVoltageAlertCount = 3;
        public int BatteryVoltageAlertCount
        {
            get => _batteryVoltageAlertCount;
            set => _batteryVoltageAlertCount = Math.Clamp(value, 1, 1000);
        }

        private int _consecutiveZerosAlertCount = 5;
        public int ConsecutiveZerosAlertCount
        {
            get => _consecutiveZerosAlertCount;
            set => _consecutiveZerosAlertCount = Math.Clamp(value, 1, 1000);
        }

        private decimal _batteryMinVoltage = 3.8m;
        public decimal BatteryMinVoltage
        {
            get => _batteryMinVoltage;
            set => _batteryMinVoltage = Math.Clamp(value, 0.0m, 4.2m);
        }

        private decimal _batteryMaxVoltage = 4.15m;
        public decimal BatteryMaxVoltage
        {
            get => _batteryMaxVoltage;
            set => _batteryMaxVoltage = Math.Clamp(value, 0.0m, 4.2m);
        }

        public bool EnableEmailAlerts { get; set; } = false;
    }
}
