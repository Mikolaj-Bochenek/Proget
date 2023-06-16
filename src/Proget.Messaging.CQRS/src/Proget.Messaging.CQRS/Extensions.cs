namespace Proget.Messaging.CQRS;

public static class Extensions
{
    public static IMessageSubscriber SubscribeEvent<TEvent>(this IMessageSubscriber subscriber)
        where TEvent : class, IMessage, IEvent
        => subscriber.Subscribe<TEvent>(async (serviceProvider, @event) => {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>().HandleAsync(@event);
        });
}
