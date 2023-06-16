namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

internal sealed class RabbitMqOptionsBuilder : IRabbitMqOptionsBuilder
{
    private readonly RabbitMqOptions _rabbitMqOptions = new();

    public RabbitMqOptions Build() => _rabbitMqOptions;

    public IRabbitMqOptionsBuilder SetMessagePersistence(bool value = true)
    {
        _rabbitMqOptions.MessagePersisted = value;
        return this;
    }

    public IRabbitMqOptionsBuilder SetRoutingKey(string? value)
    {
        _rabbitMqOptions.RoutingKey = value;
        return this;
    }

    public IRabbitMqOptionsBuilder SetLogger(bool value = true)
    {
        _rabbitMqOptions.Logger = value;
        return this;
    }

    public IRabbitMqOptionsBuilder SetConnection(Func<IConnectionOptionsBuilder, IConnectionOptionsBuilder> connection)
    {
       _rabbitMqOptions.Connection = connection(new ConnectionOptionsBuilder()).Build();
       return this;
    }

    public IRabbitMqOptionsBuilder SetExchange(Func<IExchangeOptionsBuilder, IExchangeOptionsBuilder> exchange)
    {
       _rabbitMqOptions.Exchange = exchange(new ExchangeOptionsBuilder()).Build();
       return this;
    }

    public IRabbitMqOptionsBuilder SetConventions(Func<IConventionsOptionsBuilder, IConventionsOptionsBuilder> conventions)
    {
       _rabbitMqOptions.Conventions = conventions(new ConventionsOptionsBuilder()).Build();
       return this;
    }

    public IRabbitMqOptionsBuilder SetQueue(Func<IQueueOptionsBuilder, IQueueOptionsBuilder> queue)
    {
        _rabbitMqOptions.Queue = queue(new QueueOptionsBuilder()).Build();
        return this;
    }

    public IRabbitMqOptionsBuilder SetQos(Func<IQosOptionsBuilder, IQosOptionsBuilder> queue)
    {
        _rabbitMqOptions.Qos = queue(new QosOptionsBuilder()).Build();
        return this;
    }

    public IRabbitMqOptionsBuilder SetAck(Func<IAckOptionsBuilder, IAckOptionsBuilder> ack)
    {
        _rabbitMqOptions.Ack = ack(new AckOptionsBuilder()).Build();
        return this;
    }
}