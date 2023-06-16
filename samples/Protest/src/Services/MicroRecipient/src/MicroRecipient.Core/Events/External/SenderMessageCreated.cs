using Proget.Messaging;

namespace MicroRecipient.Core.Events.External;

[Message("bootstraper", "MessageCreated")]
public sealed record SenderMessageCreated(Guid Id, int Code, string Name) : IEvent, IMessage;
