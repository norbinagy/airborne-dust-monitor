using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Statistics
{
    public interface IStatisticCalculator
    {
        StatisticCalculatorType Type { get; }
        decimal Value { get; }
        void AddSample(decimal sample);
    }
}
