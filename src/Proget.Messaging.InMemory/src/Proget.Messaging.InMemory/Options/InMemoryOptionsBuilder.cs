namespace Proget.Messaging.InMemory.Options;

internal sealed class InMemoryOptionsBuilder : IInMemoryOptionsBuilder
{
    private readonly InMemoryOptions _inMemoryOptions = new();
    
    public InMemoryOptions Build()
        => _inMemoryOptions;

    public IInMemoryOptionsBuilder SetLogger(bool value = true)
    {
        _inMemoryOptions.Logger = value;
        return this;
    }

    public IInMemoryOptionsBuilder SetExchange(string? value)
    {
        _inMemoryOptions.Exchange = value;
        return this;
    }

    public IInMemoryOptionsBuilder SetRoutingKey(string? value)
    {
        _inMemoryOptions.RoutingKey = value;
        return this;
    }

    public IInMemoryOptionsBuilder SetIgnoreRoutingKeyAttribute(bool value = true)
    {
        _inMemoryOptions.IgnoreRoutingKeyAttribute = value;
        return this;
    }
}
