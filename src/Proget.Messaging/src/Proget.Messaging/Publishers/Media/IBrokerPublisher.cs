namespace Proget.Messaging.Publishers.Media;

public interface IBrokerPublisher
{
    Task PublishAsync<TMessage>(TMessage message, string? messageId = null, string? correlationId = null)
        where TMessage : class, IMessage;
}
