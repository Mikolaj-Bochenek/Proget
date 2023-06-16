namespace Proget.Messaging.InMemory.Channels;

internal interface IChannelDispatcher
{
    Task WriteAsync(MessageEnvelope messageEnvelope, CancellationToken cancellationToken = default);
    Task ReadAsync(MessageEnvelope messageEnvelope, CancellationToken cancellationToken = default);
}
