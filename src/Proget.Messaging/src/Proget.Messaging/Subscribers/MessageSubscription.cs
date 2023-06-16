namespace Proget.Messaging.Subscribers;

internal sealed class MessageSubscription : IMessageSubscription
{
    public Type Type { get; }
    public Func<IServiceProvider, object, Task> Callback { get; }

    public MessageSubscription(Type type, Func<IServiceProvider, object, Task> callback)
    {
        Type = type;
        Callback = callback;
    }
}
