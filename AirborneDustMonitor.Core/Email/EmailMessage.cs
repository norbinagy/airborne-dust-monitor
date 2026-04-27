namespace AirborneDustMonitor.Core.Email
{
    public class EmailMessage
    {
        public string To { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Body { get; set; } = default!;
    }
}
