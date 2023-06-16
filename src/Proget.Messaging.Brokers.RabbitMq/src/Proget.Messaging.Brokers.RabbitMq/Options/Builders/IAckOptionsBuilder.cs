namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public interface IAckOptionsBuilder
{
    AckOptions Build();
    IAckOptionsBuilder SetMultipleAck(bool value = true);
    IAckOptionsBuilder SetRequeueRejected(bool value = true);
    IAckOptionsBuilder SetMultipleNack(bool value = true);
    IAckOptionsBuilder SetPublisherBasicAck(bool value = true);
    IAckOptionsBuilder SetPublisherComplexAck(bool value = true);
}
  