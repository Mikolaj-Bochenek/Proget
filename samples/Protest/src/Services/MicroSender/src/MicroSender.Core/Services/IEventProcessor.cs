namespace MicroSender.Core.Services;

public interface IEventProcessor
{
    Task ProcessAsync(IEnumerable<IDomainEvent> domainEvents);
    Task ProcessAsync(params IDomainEvent[] domainEvents);
}
