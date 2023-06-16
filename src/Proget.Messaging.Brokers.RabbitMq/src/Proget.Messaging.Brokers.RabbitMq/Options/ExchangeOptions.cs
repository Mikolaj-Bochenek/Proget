namespace Proget.Messaging.Brokers.RabbitMq.Options;

public sealed class ExchangeOptions
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public bool PublisherDeclare { get; set; }
    public bool SubscriberDeclare { get; set; }
    public bool Durable { get; set; }
    public bool AutoDelete { get; set; }
}
