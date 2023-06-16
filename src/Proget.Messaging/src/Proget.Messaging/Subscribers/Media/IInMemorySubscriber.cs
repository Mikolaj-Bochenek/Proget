namespace Proget.Messaging.Subscribers;

public interface IInMemorySubscriber
{
    void Subscribe(IMessageSubscription subscription);
}
