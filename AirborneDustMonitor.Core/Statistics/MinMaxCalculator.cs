namespace AirborneDustMonitor.Core.Statistics
{
    public class MinMaxCalculator : IStatisticCalculator
    {
        public decimal Min { get; private set; } = decimal.MaxValue;
        public decimal Max { get; private set; } = decimal.MinValue;
        public void AddSample(decimal sample)
        {
            if (sample < Min) Min = sample;
            if (sample > Max) Max = sample;
        }
    }
}
