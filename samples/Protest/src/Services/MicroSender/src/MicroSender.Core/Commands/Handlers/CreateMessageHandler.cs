namespace MicroSender.Core.Commands.Handlers;

public class CreateMessageHandler : ICommandHandler<CreateMessage>
{
    private readonly IEventProcessor _eventProcessor;
    private readonly IMicroSenderRepository _MicroSenderRepository;

    public CreateMessageHandler(IEventProcessor eventProcessor, IMicroSenderRepository MicroSenderRepository)
        => (_eventProcessor, _MicroSenderRepository) = (eventProcessor, MicroSenderRepository);

    public async Task HandleAsync(CreateMessage command, CancellationToken cancellationToken = default)
    {
        var MicroSenderMessage = new MicroSenderMessage(command.Id, command.Code, command.Name);

        var domainEvent = new MessageCreated(MicroSenderMessage.Id, MicroSenderMessage.Code, MicroSenderMessage.Name);

        // await _MicroSenderRepository.CreateAsync(MicroSenderMessage);

        await _eventProcessor.ProcessAsync(domainEvent);
    }
}
