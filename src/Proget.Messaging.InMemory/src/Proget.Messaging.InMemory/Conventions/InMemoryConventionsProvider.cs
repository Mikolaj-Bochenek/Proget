namespace Proget.Messaging.InMemory.Conventions;

internal sealed class InMemoryConventionsProvider : IConventionsProvider
{
    private readonly IConventionsBuilder _builder;

    public InMemoryConventionsProvider(IConventionsBuilder builder)
        => _builder = builder;

    public IMessageConventions Get<TMessage>() where TMessage : class, IMessage
        => Get(typeof(TMessage));

    public IMessageConventions Get(Type type)
        => new MessageConventions(type, _builder.GetRoutingKey(type), _builder.GetExchange(type), _builder.GetQueue(type));
}
