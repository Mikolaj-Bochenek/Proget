namespace Proget.Messaging.Brokers.RabbitMq.Options;

public sealed class QueueOptions
{
    public string? Template { get; set; }
    public bool Declare { get; set; }
    public bool Durable { get; set; }
    public bool Exclusive { get; set; }
    public bool AutoDelete { get; set; }
}
