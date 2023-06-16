namespace Proget.Messaging.Brokers.RabbitMq.Options;

public sealed class QosOptions
{
    public uint PrefetchSize { get; set; }
    public ushort PrefetchCount { get; set; }
    public bool Global { get; set; }
}
