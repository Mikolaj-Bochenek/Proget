using Proget.Abstractions.Events;

namespace Proget.Abstractions.Entities;

public abstract class AggregateRoot<T> where T : struct
{
    public T Id { get; protected set; }
    public uint Version { get; protected set; }
    public IEnumerable<IDomainEvent> Events => _events;

    private readonly ISet<IDomainEvent> _events = new HashSet<IDomainEvent>();
    private bool _isVersionIncremented = false;

    protected void AddEvent(IDomainEvent @event)
    {
        IncrementVersion();
        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();
    
    public void ResetVersion() => Version = 0;

    private void IncrementVersion()
    {
        if (_isVersionIncremented)
            return;

        Version++;
        _isVersionIncremented = true;
    }
}
