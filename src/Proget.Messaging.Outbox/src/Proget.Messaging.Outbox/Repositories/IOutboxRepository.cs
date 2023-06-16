namespace Proget.Messaging.Outbox;

public interface IOutboxRepository
{
    Task<IReadOnlyList<OutboxMessage>> GetUnsentAsync();
    Task ProcessAsync(OutboxMessage message);
    Task ProcessAsync(IEnumerable<OutboxMessage> messages);
    Task InsertAsync<TMessage>(TMessage message, string? messageId = null) where TMessage : class, IMessage;
}
