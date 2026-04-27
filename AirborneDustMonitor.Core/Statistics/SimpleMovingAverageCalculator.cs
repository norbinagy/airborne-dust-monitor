using AirborneDustMonitor.Core.Interfaces;

namespace AirborneDustMonitor.Core.Statistics
{
    public class SimpleMovingAverageCalculator : IStatisticCalculator
    {
        private readonly Queue<decimal> _dataQueue;
        private readonly int _windowSize;
        private decimal _sum;
        public decimal Average
        {
            get
            {
                return _dataQueue.Count > 0 ? _sum / _dataQueue.Count : 0;
            }
        }

        public SimpleMovingAverageCalculator(IAppSettingsService appSettingsService)
        {
            _dataQueue = new Queue<decimal>();
            this._windowSize = appSettingsService.Current.Data.SimpleMovingAverageWindowSize;
            _sum = 0;
        }

        public void AddSample(decimal sample)
        {
            if (_dataQueue.Count == _windowSize)
            {
                _sum -= _dataQueue.Dequeue();
            }
            _dataQueue.Enqueue(sample);
            _sum += sample;
        }
    }
}
