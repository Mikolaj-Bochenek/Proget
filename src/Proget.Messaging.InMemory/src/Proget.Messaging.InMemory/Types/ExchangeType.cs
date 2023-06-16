namespace Proget.Messaging.InMemory.Types;

internal static class ExchangeType
{
    public const string Direct = "direct";
    public const string Fanout = "fanout";
    public const string Headers = "headers";
    public const string Topic = "topic";
    internal static ICollection<string> All() => new [] { Direct, Fanout, Headers, Topic };
}