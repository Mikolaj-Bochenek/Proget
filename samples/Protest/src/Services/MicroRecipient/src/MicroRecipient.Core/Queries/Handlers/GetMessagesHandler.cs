namespace MicroRecipient.Core.Queries.Handlers;

public sealed class GetMessagesHandler : IQueryHandler<GetMessages, IEnumerable<RecipientMessage>>
{
    private readonly IRecipientRepository _repository;

    public GetMessagesHandler(IRecipientRepository repository)
        => _repository = repository;

    public async Task<IEnumerable<RecipientMessage>> HandleAsync(GetMessages query, CancellationToken cancellationToken = default)
        => await _repository.GetAll();
}
