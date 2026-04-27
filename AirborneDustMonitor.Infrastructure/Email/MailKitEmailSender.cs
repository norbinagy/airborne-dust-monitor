using AirborneDustMonitor.Core.Email;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace AirborneDustMonitor.Infrastructure.Email
{
    public class MailKitEmailSender : IEmailSender
    {
        private readonly EmailSettings _settings;

        public MailKitEmailSender(IAppSettingsService appSettingsService)
        {
            _settings = appSettingsService.Current.Email;
        }

        public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_settings.From));
            email.To.Add(MailboxAddress.Parse(message.To));
            email.Subject = message.Subject;

            email.Body = new TextPart("plain")
            {
                Text = message.Body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _settings.Host,
                _settings.Port,
                SecureSocketOptions.StartTls,
                cancellationToken);

            await smtp.AuthenticateAsync(
                _settings.Username,
                _settings.Password,
                cancellationToken);

            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }
    }
}
