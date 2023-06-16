namespace Proget.Messaging.Email.Publishers.Media;

public interface ISmtpPublisher
{
    Task PublishAsync(EmailMessage message, string? messageId = null);
}
