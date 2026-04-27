namespace AirborneDustMonitor.Core.Settings
{
    public class DataSettings
    {
        private int _simpleMovingAverageWindowSize = 3;
        public int SimpleMovingAverageWindowSize
        {
            get => _simpleMovingAverageWindowSize;
            set => _simpleMovingAverageWindowSize = Math.Clamp(value, 1, 100);
        }
    }
}
