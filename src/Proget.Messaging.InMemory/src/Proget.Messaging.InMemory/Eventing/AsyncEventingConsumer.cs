namespace Proget.Messaging.InMemory.Eventing;

internal sealed class AsyncEventingConsumer : IAsyncEventingConsumer
{
    public event AsyncEventHandler<DeliveryEventArgs>? Received;

    public void Add(MessageEnvelope messageEnvelope)
        => Received?.Invoke(new DeliveryEventArgs(messageEnvelope));
}
