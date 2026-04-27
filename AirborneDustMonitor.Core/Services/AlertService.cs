using AirborneDustMonitor.Core.Email;
using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Rules;

namespace AirborneDustMonitor.Core.Services
{
    // Az AlertService a regisztrált szabályok (Rule) alapján értékeli a bejövő mintákat, és szükség esetén riasztásokat generál eseményként.
    public class AlertService
    {
        private readonly IEmailSender _emailSender;
        private bool _isEmailAlertActive;
        public event Action<Alert>? AlertRaised;
        private readonly Dictionary<Type, IAlertRule> _alertRules;
        private readonly string _emailTo;

        public AlertService(IEmailSender emailSender, IAppSettingsService appSettingsService)
        {
            _alertRules = new Dictionary<Type, IAlertRule>();
            RegisterRule(new ThresholdRule());
            RegisterRule(new PeakRule());
            RegisterRule(new BatteryVoltageRule(appSettingsService));
            RegisterRule(new ConsecutiveZeroValuesRule(appSettingsService));
            this._emailSender = emailSender;
            _emailTo = appSettingsService.Current.Email.To;
            _isEmailAlertActive = appSettingsService.Current.Alert.EnableEmailAlerts;
        }

        // A ProcessSample metódus a bejövő mintákat értékeli a regisztrált szabályok alapján, és ha egy szabály nem null Alert-et ad vissza, azt kiváltja illetve emailt küld(kivéve csúcsértékekről), ha aktív a beállítás.
        public void ProcessSample(MetricType metricType, int sensorID, decimal value)
        {
            foreach (var rule in _alertRules)
            {
                var ruleType = rule.Key;
                var ruleInstance = rule.Value;

                var alert = ruleInstance.Evaluate(metricType, sensorID, value);
                if (alert != null)
                {
                    AlertRaised?.Invoke(alert);

                    if ((ruleType == typeof(BatteryVoltageRule) || ruleType == typeof(ConsecutiveZeroValuesRule) || ruleType == typeof(ThresholdRule)) && _isEmailAlertActive)
                    {
                        if (ruleInstance is ThresholdRule thresholdRule && thresholdRule.IsEmailPending(metricType))
                        {
                            thresholdRule.MarkEmailSent(metricType);
                        }
                        HandleEmailAlertAsync(alert).Wait();
                    }
                }
            }
        }

        // A HandleEmailAlertAsync metódus aszinkron módon küldi el az emailt a megadott címre, a riasztás üzenetével és időpontjával.
        public async Task HandleEmailAlertAsync(Alert alert)
        {
            EmailMessage email = new EmailMessage
            {
                To = _emailTo,
                Subject = "Szenzor riasztás!",
                Body = $"{alert.Message}\nIdőpont: {alert.Date}"
            };
            await _emailSender.SendAsync(email);
        }

        // A RegisterRule metódus regisztrál egy új szabályt az AlertService-ben.
        private void RegisterRule<T>(T rule) where T : IAlertRule
        {
            _alertRules[typeof(T)] = rule;
        }

        // A GetRule metódus visszaadja a megadott típusú szabályt, ha az regisztrálva van, különben null értéket ad vissza.
        public T? GetRule<T>() where T : class, IAlertRule
        {
            if (_alertRules.TryGetValue(typeof(T), out var rule))
            {
                return rule as T;
            }
            return null;
        }
    }
}
