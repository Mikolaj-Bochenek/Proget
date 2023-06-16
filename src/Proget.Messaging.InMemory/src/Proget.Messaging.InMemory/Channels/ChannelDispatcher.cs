namespace Proget.Messaging.InMemory.Channels;

internal sealed class ChannelDispatcher : IChannelDispatcher
{
    private readonly IChannelAccessor _channelAccessor;

    public ChannelDispatcher(IChannelAccessor channelAccessor)
        => _channelAccessor = channelAccessor;

    public async Task WriteAsync(MessageEnvelope messageEnvelope, CancellationToken cancellationToken)
        => await _channelAccessor.Writer.WriteAsync(messageEnvelope, cancellationToken);
    public async Task ReadAsync(MessageEnvelope messageEnvelope, CancellationToken cancellationToken)
        => await _channelAccessor.Reader.ReadAsync(cancellationToken);

}
