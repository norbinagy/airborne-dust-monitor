using AirborneDustMonitor.Core.Email;
using AirborneDustMonitor.Core.Interfaces.Configuration;
using AirborneDustMonitor.Core.Interfaces.Email;
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

            if(DataProtectionHelper.TryUnprotect(_settings.ProtectedPassword, out var unprotectedPassword))
            {
                await smtp.AuthenticateAsync(
                    _settings.Username,
                    unprotectedPassword,
                    cancellationToken);
            }

            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }
    }
}
