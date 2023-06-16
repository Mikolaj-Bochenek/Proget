using Sender.Core.Domain.Events;

namespace Sender.Core.Services;

internal sealed class EventToMessageMapper : IEventToMessageMapper
{
    public IMessage? Map(IDomainEvent @event) => @event switch
    {
        MessageCreated domainEvent => new Messages.MessageCreated(domainEvent.Id, domainEvent.Code, domainEvent.Name),
        _ => null
    };

    public IEnumerable<IMessage?> MapAll(IEnumerable<IDomainEvent> domainEvents) => domainEvents.Select(Map);
}
