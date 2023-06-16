namespace Proget.Messaging.Subscribers;

public interface IBrokerSubscriber
{
    void Subscribe(IMessageSubscription subscription);
}
