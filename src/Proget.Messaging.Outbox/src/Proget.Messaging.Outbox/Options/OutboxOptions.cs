namespace Proget.Messaging.Outbox.Options;

public sealed class OutboxOptions
{
    public bool Enabled { get; set; }
    public double IntervalMilliseconds { get; set; }
    public string? Type { get; set; } 
}
