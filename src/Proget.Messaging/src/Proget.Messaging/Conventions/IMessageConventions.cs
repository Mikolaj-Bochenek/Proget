namespace Proget.Messaging.Conventions;

public interface IMessageConventions
{
    Type Type { get; }
    string Exchange { get; }
    string RoutingKey { get; }
    string? Queue { get; }
}
