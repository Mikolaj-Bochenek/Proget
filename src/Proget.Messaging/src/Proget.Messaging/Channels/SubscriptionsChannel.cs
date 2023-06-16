namespace Proget.Messaging.Channels;

internal sealed class SubscriptionsChannel
{
    private readonly Channel<IMessageSubscription> _channel = Channel.CreateUnbounded<IMessageSubscription>();

    public ChannelReader<IMessageSubscription> Reader => _channel.Reader;
    public ChannelWriter<IMessageSubscription> Writer => _channel.Writer;
}