using Microsoft.Extensions.DependencyInjection;

namespace Proget.CQRS.Events.Dispatchers;

internal sealed class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IEvent
    {
        if (typeof(TEvent) == typeof(IEvent))
        {
            await DispatchDynamicallyAsync(@event, cancellationToken);
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();

        foreach (var handler in handlers)
            await handler.HandleAsync(@event, cancellationToken);
    }

    private async Task DispatchDynamicallyAsync(IEvent @event, CancellationToken cancellationToken = default)
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());

        var handlers = scope.ServiceProvider.GetServices(handlerType);

        var method = handlerType.GetMethod(nameof(IEventHandler<IEvent>.HandleAsync))
            ?? throw new InvalidOperationException($"Event handler for '{@event.GetType().Name}' is invalid.");

        // PossibleNullReferenceException
        var tasks = handlers.Select(h => (Task)method.Invoke(h, new object[] { @event, cancellationToken })!);

        await Task.WhenAll(tasks);
    }
}
