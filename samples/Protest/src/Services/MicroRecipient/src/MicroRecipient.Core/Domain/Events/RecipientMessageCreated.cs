namespace MicroRecipient.Core.Domain.Events;

public sealed record RecipientMessageCreated(Guid Id, int Code, string? Name);
