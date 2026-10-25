using AirborneDustMonitor.Core.Interfaces.Rules;

namespace AirborneDustMonitor.Core.Interfaces.Services
{
    public interface IAlertRuleProvider
    {
        IReadOnlyCollection<IAlertRule> GetActiveRules();
        T? GetRule<T>() where T : class, IAlertRule;
    }
}
