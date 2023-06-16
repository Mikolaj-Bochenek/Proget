namespace Proget.Messaging.InMemory.Eventing;

internal sealed class DeliveryEventArgs : EventArgs
{
    public MessageEnvelope MessageEnvelope { get; }

    public DeliveryEventArgs(MessageEnvelope messageEnvelope)
        => MessageEnvelope = messageEnvelope;
}
