using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Statistics;

namespace AirborneDustMonitor.Core.Interfaces.Services
{
    public interface IStatisticCalculatorProvider
    {
        IReadOnlyCollection<IStatisticCalculator> GetCalculators(MetricType metricType);
    }
}
