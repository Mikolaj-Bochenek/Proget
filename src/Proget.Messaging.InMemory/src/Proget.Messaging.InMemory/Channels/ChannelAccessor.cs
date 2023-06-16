namespace Proget.Messaging.InMemory.Channels;

internal sealed class ChannelAccessor : IChannelAccessor
{
    private Channel<MessageEnvelope> _channel = Channel.CreateUnbounded<MessageEnvelope>();

    public ChannelReader<MessageEnvelope> Reader => _channel.Reader;
    public ChannelWriter<MessageEnvelope> Writer => _channel.Writer;
}
