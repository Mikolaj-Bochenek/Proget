namespace Proget.Messaging.Brokers.RabbitMq.Options;

public sealed class RabbitMqOptions
{
    public ConnectionOptions Connection { get; set; }
    public ExchangeOptions Exchange { get; set; }
    public QueueOptions Queue { get; set; }
    public ConventionsOptions Conventions { get; set; }
    public QosOptions Qos { get; set; }
    public AckOptions Ack { get; set; }
    public bool Logger { get; set; }
    public bool MessagePersisted { get; set; }
    public string? RoutingKey { get; set; }

    public RabbitMqOptions()
    {
        Connection = new ConnectionOptions();
        Exchange = new ExchangeOptions();
        Queue = new QueueOptions();
        Conventions = new ConventionsOptions();
        Qos = new QosOptions();
        Ack = new AckOptions();
    }
}
