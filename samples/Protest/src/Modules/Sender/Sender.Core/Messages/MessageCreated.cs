using Proget.Messaging;

namespace Sender.Core.Messages;

[Message(routingKey: "europe.poland")]
public sealed record MessageCreated(Guid Id, int Code, string? Name) : IMessage, IEvent;