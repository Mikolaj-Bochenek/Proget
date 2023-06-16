namespace ModularRecipient.Core.Events.Handlers;

public sealed class SenderMessageCreatedHandler2 : IEventHandler<SenderMessageCreated2>
{
    private readonly ILogger<SenderMessageCreatedHandler2> _logger;
    private readonly IRecipientRepository _repository;

    public SenderMessageCreatedHandler2(IRecipientRepository repository, ILogger<SenderMessageCreatedHandler2> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task HandleAsync(SenderMessageCreated2 @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("GERMANY RECEIVED!");

        var recipientMessage = new RecipientMessage(@event.Id, @event.Code, @event.Name);

        await _repository.CreateAsync(recipientMessage);

        var domainEvent = new RecipientMessageCreated(recipientMessage.Id, recipientMessage.Code, recipientMessage.Name);

        _logger.LogInformation($"Recipient message created 2 [id: '{domainEvent.Id}']");
    }
}
