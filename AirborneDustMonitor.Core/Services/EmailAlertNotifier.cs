using AirborneDustMonitor.Core.Email;
using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Configuration;
using AirborneDustMonitor.Core.Interfaces.Email;
using AirborneDustMonitor.Core.Interfaces.Services;

namespace AirborneDustMonitor.Core.Services
{
    public class EmailAlertNotifier : IAlertNotifier
    {
        private readonly IEmailSender _emailSender;
        private readonly IAppSettingsService _appSettingsService;

        public EmailAlertNotifier(IEmailSender emailSender, IAppSettingsService appSettingsService)
        {
            _emailSender = emailSender;
            _appSettingsService = appSettingsService;
        }

        public async Task NotifyAsync(Alert alert)
        {
            if (!_appSettingsService.Current.Alert.EnableEmailAlerts)
            {
                return;
            }

            var email = new EmailMessage
            {
                To = _appSettingsService.Current.Email.To,
                Subject = "Szenzor riasztás!",
                Body = $"{alert.Message}\nIdőpont: {alert.Date}"
            };

            await _emailSender.SendAsync(email);
        }
    }
}
