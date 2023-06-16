namespace Proget.Messaging.Subscribers;

public interface IMessageSubscription
{
    public Type Type { get; }
    public Func<IServiceProvider, object, Task> Callback { get; }
}
