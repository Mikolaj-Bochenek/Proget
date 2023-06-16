namespace Proget.Messaging.Publishers;

public interface IMessagePublisher
{
    Task PublishAsync<TMessage>(TMessage message, string? messageId = null)
        where TMessage : class, IMessage;
}
