namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public interface IRabbitMqOptionsBuilder : IProgetOptionsBuilder<RabbitMqOptions>
{
    IRabbitMqOptionsBuilder SetLogger(bool value = true);
    IRabbitMqOptionsBuilder SetMessagePersistence(bool value = true);
    IRabbitMqOptionsBuilder SetRoutingKey(string? value);
    IRabbitMqOptionsBuilder SetConnection(Func<IConnectionOptionsBuilder, IConnectionOptionsBuilder> connection);
    IRabbitMqOptionsBuilder SetExchange(Func<IExchangeOptionsBuilder, IExchangeOptionsBuilder> exchange);
    IRabbitMqOptionsBuilder SetQueue(Func<IQueueOptionsBuilder, IQueueOptionsBuilder> queue);
    IRabbitMqOptionsBuilder SetConventions(Func<IConventionsOptionsBuilder, IConventionsOptionsBuilder> conventions);
    IRabbitMqOptionsBuilder SetQos(Func<IQosOptionsBuilder, IQosOptionsBuilder> qos);
    IRabbitMqOptionsBuilder SetAck(Func<IAckOptionsBuilder, IAckOptionsBuilder> qos);
}