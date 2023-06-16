using Proget.Messaging;

namespace ModularRecipient.Core.Events.External;

[Message(routingKey: "europe.germany")]
public sealed record SenderMessageCreated2(Guid Id, int Code, string Name) : IEvent, IMessage;
