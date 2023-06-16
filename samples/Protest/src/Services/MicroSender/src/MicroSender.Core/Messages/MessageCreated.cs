using Proget.Messaging;

namespace MicroSender.Core.Messages;

[Message(null, "rampampam")]
public sealed record MessageCreated(Guid Id, int Code, string? Name) : IMessage, IEvent;