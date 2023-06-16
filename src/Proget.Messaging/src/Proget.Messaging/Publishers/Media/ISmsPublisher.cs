namespace Proget.Messaging.Publishers.Media;

public interface ISmsPublisher
{
    Task PublishAsync<TMessage>(TMessage message, string? messageId = null)
        where TMessage : class, IMessage;
}
