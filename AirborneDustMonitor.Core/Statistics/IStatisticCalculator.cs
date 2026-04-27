namespace AirborneDustMonitor.Core.Statistics
{
    public interface IStatisticCalculator
    {
        void AddSample(decimal sample);
    }
}
