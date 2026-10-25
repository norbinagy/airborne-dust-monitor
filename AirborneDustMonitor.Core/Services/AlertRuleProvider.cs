using AirborneDustMonitor.Core.Interfaces.Rules;
using AirborneDustMonitor.Core.Interfaces.Services;

namespace AirborneDustMonitor.Core.Services
{
    public class AlertRuleProvider : IAlertRuleProvider
    {
        private readonly IReadOnlyCollection<IAlertRule> _rules;

        public AlertRuleProvider(IEnumerable<IAlertRule> rules)
        {
            _rules = rules.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<IAlertRule> GetActiveRules()
        {
            return _rules;
        }

        public T? GetRule<T>() where T : class, IAlertRule
        {
            return _rules.OfType<T>().FirstOrDefault();
        }
    }
}
