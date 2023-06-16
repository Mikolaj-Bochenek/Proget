namespace Proget.Messaging.Options;

public sealed class MessagingOptions
{
    public bool BrokerEnabled { get; set; }
    public bool NotificationEnabled { get; set; }
    public bool MailingEnabled { get; set; }
    public bool InMemoryEnabled { get; set; }
    public bool SmsEnabled { get; set; }
}
