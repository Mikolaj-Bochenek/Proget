namespace MicroSender.Core.Services;

public interface IEventToMessageMapper
{
    IMessage? Map(IDomainEvent @event);
    IEnumerable<IMessage?> MapAll(IEnumerable<IDomainEvent> events);
}
