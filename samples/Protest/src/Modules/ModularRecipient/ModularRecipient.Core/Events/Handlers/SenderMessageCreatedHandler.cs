namespace ModularRecipient.Core.Events.Handlers;

public sealed class SenderMessageCreatedHandler : IEventHandler<SenderMessageCreated>
{
    private readonly ILogger<SenderMessageCreatedHandler> _logger;
    private readonly IRecipientRepository _repository;

    public SenderMessageCreatedHandler(IRecipientRepository repository, ILogger<SenderMessageCreatedHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task HandleAsync(SenderMessageCreated @event, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("POLAND RECEIVED!");

        var recipientMessage = new RecipientMessage(@event.Id, @event.Code, @event.Name);

        await _repository.CreateAsync(recipientMessage);

        var domainEvent = new RecipientMessageCreated(recipientMessage.Id, recipientMessage.Code, recipientMessage.Name);

        _logger.LogInformation($"Recipient message created [id: '{domainEvent.Id}']");
    }
}
