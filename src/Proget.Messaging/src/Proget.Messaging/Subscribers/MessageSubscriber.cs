namespace Proget.Messaging.Subscribers;

internal sealed class MessageSubscriber : IMessageSubscriber
{
    private readonly SubscriptionsChannel _subscriptionsChannel;

    public MessageSubscriber(SubscriptionsChannel subscriptionsChannel)
        => _subscriptionsChannel = subscriptionsChannel;

    public IMessageSubscriber Subscribe<TMessage>(Func<IServiceProvider, TMessage, Task> callback)
        where TMessage : class, IMessage
    {
        var type = typeof(TMessage);
        var subscription = new MessageSubscription(type, (sp, msg) => callback(sp, (TMessage)msg));

        _subscriptionsChannel.Writer.TryWrite(subscription);

        return this;
    }
}
