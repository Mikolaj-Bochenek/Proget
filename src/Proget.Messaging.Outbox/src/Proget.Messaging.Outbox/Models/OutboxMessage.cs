namespace Proget.Messaging.Outbox.Models;

public abstract class OutboxMessage : IMessage
{
    public string? Id { get; set; }
    public string? MessageType { get; set; }
    public string? SerializedMessage { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}
