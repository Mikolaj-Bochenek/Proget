namespace Sender.Core.Commands.Handlers;

public class CreateMessageHandler : ICommandHandler<CreateMessage>
{
    private readonly IEventProcessor _eventProcessor;
    private readonly ISenderRepository _senderRepository;

    public CreateMessageHandler(IEventProcessor eventProcessor, ISenderRepository senderRepository)
        => (_eventProcessor, _senderRepository) = (eventProcessor, senderRepository);

    public async Task HandleAsync(CreateMessage command, CancellationToken cancellationToken = default)
    {
        var senderMessage = new SenderMessage(command.Id, command.Code, command.Name);

        var domainEvent = new MessageCreated(senderMessage.Id, senderMessage.Code, senderMessage.Name);

        await _senderRepository.CreateAsync(senderMessage);

        await _eventProcessor.ProcessAsync(domainEvent);
    }
}
