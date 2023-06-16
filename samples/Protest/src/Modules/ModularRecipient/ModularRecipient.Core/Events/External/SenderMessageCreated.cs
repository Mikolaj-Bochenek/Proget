using Proget.Messaging;

namespace ModularRecipient.Core.Events.External;

[Message(routingKey: "europe.*")]
public sealed record SenderMessageCreated(Guid Id, int Code, string Name) : IEvent, IMessage;
