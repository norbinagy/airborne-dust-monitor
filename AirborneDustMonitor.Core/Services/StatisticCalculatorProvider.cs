using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Configuration;
using AirborneDustMonitor.Core.Interfaces.Services;
using AirborneDustMonitor.Core.Interfaces.Statistics;
using AirborneDustMonitor.Core.Statistics;

namespace AirborneDustMonitor.Core.Services
{
    public class StatisticCalculatorProvider : IStatisticCalculatorProvider
    {
        private readonly IAppSettingsService _appSettingsService;

        public StatisticCalculatorProvider(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        public IReadOnlyCollection<IStatisticCalculator> GetCalculators(MetricType metricType)
        {
            return new List<IStatisticCalculator>
            {
                new MinCalculator(),
                new MaxCalculator(),
                new SimpleMovingAverageCalculator(_appSettingsService)
            };
        }
    }
}
