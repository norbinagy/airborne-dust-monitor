using AirborneDustMonitor.Core.Email;

namespace AirborneDustMonitor.Core.Interfaces.Email
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
    }
}
