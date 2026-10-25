using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Rules;
using AirborneDustMonitor.Core.Interfaces.Services;

namespace AirborneDustMonitor.Core.Services
{
    // Az AlertService a regisztrált szabályok (Rule) alapján értékeli a bejövő mintákat, és szükség esetén riasztásokat generál eseményként.
    public class AlertService : IAlertService
    {
        private readonly IAlertRuleProvider _ruleProvider;
        private readonly IReadOnlyCollection<IAlertNotifier> _notifiers;
        public event Action<Alert>? AlertRaised;

        public AlertService(IAlertRuleProvider ruleProvider, IEnumerable<IAlertNotifier> notifiers)
        {
            _ruleProvider = ruleProvider;
            _notifiers = notifiers.ToList().AsReadOnly();
        }

        // A ProcessSample metódus a bejövő mintákat értékeli a regisztrált szabályok alapján, és ha egy szabály nem null Alert-et ad vissza, azt kiváltja és értesítést küld a notifier komponenseken keresztül.
        public async Task ProcessSampleAsync(MetricType metricType, int sensorID, decimal value)
        {
            foreach (var rule in _ruleProvider.GetActiveRules())
            {
                var alert = rule.Evaluate(metricType, sensorID, value);
                if (alert != null)
                {
                    AlertRaised?.Invoke(alert);

                    if (rule.NotifyByEmail)
                    {
                        foreach (var notifier in _notifiers)
                        {
                            await notifier.NotifyAsync(alert);
                        }
                    }
                }
            }
        }

        // A GetRule metódus visszaadja a megadott típusú szabályt, ha elérhető az aktív szabályok között.
        public T? GetRule<T>() where T : class, IAlertRule
        {
            return _ruleProvider.GetRule<T>();
        }
    }
}
