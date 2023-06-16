namespace Proget.Messaging.Brokers.RabbitMq.Connections;

public interface IChannelFactory
{
    IModel Create();
}
