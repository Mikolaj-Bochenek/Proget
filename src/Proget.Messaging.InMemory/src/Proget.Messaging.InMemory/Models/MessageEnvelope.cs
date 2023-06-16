namespace Proget.Messaging.InMemory.Models;

internal sealed record MessageEnvelope(
    string MessageId,
    string Exchange,
    string RoutingKey,
    long Timestamp, 
    Dictionary<string, object> headers,
    byte[] Body
);
