namespace MicroSender.Core.Services;

internal sealed class MessageProcessor : IMessageProcessor
{
    private readonly IMessagePublisher _messagePublisher;
    // private readonly IOutboxRepository _outbox;
    // private readonly OutboxOptions _outboxOptions;

    public MessageProcessor(IMessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public async Task ProcessAsync(params IMessage[] messages)
        => await ProcessAsync(messages?.AsEnumerable());

    public async Task ProcessAsync(IEnumerable<IMessage>? messages)
    {
        if (messages is null)
            return;
        
        foreach (var message in messages)
        {
            if (message is null)
                continue;
            
            var messageId = Guid.NewGuid().ToString("N");

            // if (_outboxOptions.Enabled)
            // {
            //     await _outbox.InsertAsync(message, messageId);
            //     continue;
            // }

            await _messagePublisher.PublishAsync(message, messageId);
        }
    }
}
