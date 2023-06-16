namespace MicroSender.Core.Services;

internal sealed class EventProcessor : IEventProcessor
{
    private readonly IMessageProcessor _messageProcessor;
    private readonly IEventToMessageMapper _mapper;
    private readonly ILogger<EventProcessor> _logger;

    public EventProcessor(IMessageProcessor messageProcessor, IEventToMessageMapper mapper, ILogger<EventProcessor> logger)
    {
        _messageProcessor = messageProcessor;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task ProcessAsync(IEnumerable<IDomainEvent>? domainEvents)
    {
        if (domainEvents is null)
            return;

        _logger.LogInformation($"Processing domain events {typeof(IDomainEvent).Name}...");
        
        var messages = _mapper.MapAll(domainEvents);

        foreach (var message in messages)
            if (message is not null)
                await _messageProcessor.ProcessAsync(message);
    }

    public async Task ProcessAsync(params IDomainEvent[] domainEvents)
        => await ProcessAsync(domainEvents?.AsEnumerable());
}
