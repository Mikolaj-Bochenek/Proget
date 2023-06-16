namespace Proget.Messaging.Conventions;

public class MessageConventions : IMessageConventions
{
    public Type Type { get; }
    public string Exchange { get; }
    public string RoutingKey { get; }
    public string? Queue { get; }

    public MessageConventions(Type type, string routingKey, string exchange, string? queue)
    {
        Type = type;
        RoutingKey = routingKey;
        Exchange = exchange;
        Queue = queue;
    }
}
