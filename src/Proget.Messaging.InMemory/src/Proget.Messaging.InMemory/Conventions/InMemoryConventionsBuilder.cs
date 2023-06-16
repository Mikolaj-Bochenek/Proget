using Proget.Messaging.InMemory.Types;

namespace Proget.Messaging.InMemory.Conventions;

internal sealed class InMemoryConventionsBuilder : ConventionsBuilder<MessageAttribute>, IConventionsBuilder
{
    private readonly InMemoryOptions _options;
    private readonly bool _snakeCase;

    public InMemoryConventionsBuilder(InMemoryOptions options)
    {
        _options = options;
        _snakeCase = options.Casing?.Equals("snakeCase",
                StringComparison.InvariantCultureIgnoreCase) == true;

    }

    public string GetExchange(Type type)
    {
        return _options.Exchange switch
        {
            _ when string.Equals(ExchangeType.Direct, _options.Exchange, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Direct,
            _ when string.Equals(ExchangeType.Fanout, _options.Exchange, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Fanout,
            _ when string.Equals(ExchangeType.Headers, _options.Exchange, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Headers,
            _ when string.Equals(ExchangeType.Topic, _options.Exchange, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Topic,
            _ => throw new Exception("The type of exchange has to be one of these: ['direct', 'fanout', 'headers', 'topic']")
        };
    }

    public string GetRoutingKey(Type type)
    {
        var routingKey = string.IsNullOrWhiteSpace(_options.RoutingKey)
            ? type.Name
            : _options.RoutingKey;

        if (_options.IgnoreRoutingKeyAttribute is true)
        {
            return _snakeCase
                ? WithSnakeCasing(routingKey)
                : routingKey;
        }

        var attribute = GetAttribute(type);

        routingKey = string.IsNullOrWhiteSpace(attribute?.RoutingKey)
            ? routingKey
            : attribute.RoutingKey;

        return _snakeCase ? WithSnakeCasing(routingKey) : routingKey;
    }

    public string? GetQueue(Type type)
        => _snakeCase ? WithSnakeCasing(type.Name) : type.Name;
}
