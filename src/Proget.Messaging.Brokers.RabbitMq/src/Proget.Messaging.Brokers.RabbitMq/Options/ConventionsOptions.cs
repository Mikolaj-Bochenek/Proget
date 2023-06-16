namespace Proget.Messaging.Brokers.RabbitMq.Options;

public sealed class ConventionsOptions
{
    public bool IgnoreExchangeAttribute { get; set; }
    public bool IgnoreRoutingKeyAttribute { get; set; }
    public bool IgnoreQueueAttribute { get; set; }
    public string? Casing { get; set; }
}
