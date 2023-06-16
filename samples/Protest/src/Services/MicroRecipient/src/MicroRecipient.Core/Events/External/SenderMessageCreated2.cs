using Proget.Messaging;

namespace MicroRecipient.Core.Events.External;

[Message("sender-service")]
public sealed record SenderMessageCreated2(Guid Id, int Code, string Name) : IEvent, IMessage;
