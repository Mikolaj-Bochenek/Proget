namespace Proget.Messaging.InMemory.Options;

public sealed class InMemoryOptions
{
    public bool Logger { get; set; }
    public string? Exchange { get; set; }
    public string? RoutingKey { get; set; }
    public string? Casing { get; set; }
    public bool IgnoreRoutingKeyAttribute { get; set; }
}
