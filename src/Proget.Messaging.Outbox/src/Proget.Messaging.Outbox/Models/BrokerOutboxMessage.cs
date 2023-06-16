namespace Proget.Messaging.Outbox.Models;

public class BrokerOutboxMessage : OutboxMessage
{
    public DateTime? ExchangedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}
