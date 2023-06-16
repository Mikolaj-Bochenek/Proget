namespace Sender.Core.Domain.Events;

public sealed record MessageCreated(Guid Id, int Code, string? Name) : IDomainEvent;
