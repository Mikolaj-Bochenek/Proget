namespace Proget.Messaging.Brokers.RabbitMq.Options;

public sealed class AckOptions
{
    public bool MultipleAck { get; set; }
    public bool MultipleNack { get; set; }
    public bool RequeueRejected { get; set; }
    public bool PublisherBasicAckEnabled { get; set; }
    public bool PublisherComplexAckEnabled { get; set; }
}
