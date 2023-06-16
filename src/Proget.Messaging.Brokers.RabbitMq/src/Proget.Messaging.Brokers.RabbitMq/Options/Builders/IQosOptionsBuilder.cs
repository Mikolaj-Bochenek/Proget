namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public interface IQosOptionsBuilder
{
    QosOptions Build();
    IQosOptionsBuilder SetPrefetchSize(uint value);
    IQosOptionsBuilder SetPrefetchCount(ushort value);
    IQosOptionsBuilder SetGlobal(bool value = true);
}
  