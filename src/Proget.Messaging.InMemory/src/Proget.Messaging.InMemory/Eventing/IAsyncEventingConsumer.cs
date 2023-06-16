namespace Proget.Messaging.InMemory.Eventing;

internal interface IAsyncEventingConsumer
{
    event AsyncEventHandler<DeliveryEventArgs> Received;

    void Add(MessageEnvelope messageEnvelope);
}
