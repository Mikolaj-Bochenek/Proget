namespace Proget.Messaging.Publishers.Media;

public interface INotificationPublisher
{
    Task PublishAsync<TMessage>(TMessage message, string? messageId = null)
        where TMessage : class, IMessage;
}
