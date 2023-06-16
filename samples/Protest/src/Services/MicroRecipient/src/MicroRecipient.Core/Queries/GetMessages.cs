namespace MicroRecipient.Core.Queries;

public sealed record GetMessages() : IQuery<IEnumerable<RecipientMessage>>;
