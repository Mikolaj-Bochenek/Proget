namespace Proget.Messaging.Email.Publishers.Media;

public interface ISendGridPublisher
{
    Task PublishAsync(EmailMessage message, string? messageId = null);
}
