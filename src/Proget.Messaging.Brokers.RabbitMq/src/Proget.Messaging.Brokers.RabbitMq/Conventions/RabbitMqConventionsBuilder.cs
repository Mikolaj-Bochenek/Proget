namespace Proget.Messaging.Brokers.RabbitMq.Conventions;

internal sealed class RabbitMqConventionsBuilder : ConventionsBuilder<MessageAttribute>, IConventionsBuilder
{
    private readonly RabbitMqOptions _options;
    private readonly bool _snakeCase;
    private readonly string? _queueTemplate;

    public RabbitMqConventionsBuilder(RabbitMqOptions options, MessagingOptions messaingOptions)
    {
        _options = options;
        _snakeCase = options.Conventions?.Casing?.Equals("SnakeCase",
            StringComparison.InvariantCultureIgnoreCase) == true;
        _queueTemplate = string.IsNullOrWhiteSpace(_options.Queue?.Template)
            ? "{{assembly}}/{{exchange}}.{{message}}"
            : options.Queue?.Template;
    }

    public string GetExchange(Type type)
    {
        var exchange = string.IsNullOrWhiteSpace(_options.Exchange?.Name)
            ? type.Assembly.GetName().Name ?? type.Assembly.GetName().FullName
            : _options.Exchange.Name;
        
        if (_options.Conventions?.IgnoreExchangeAttribute is true)
            return _snakeCase ? WithSnakeCasing(exchange) : exchange;

        var attribute = GetAttribute(type);
        exchange = string.IsNullOrWhiteSpace(attribute?.Exchange) ? exchange : attribute.Exchange;

        return _snakeCase ? WithSnakeCasing(exchange) : exchange;
    }

    public string GetRoutingKey(Type type)
    {
        var routingKey = string.IsNullOrWhiteSpace(_options.RoutingKey)
            ? type.Name
            : _options.RoutingKey;

        if (_options.Conventions?.IgnoreRoutingKeyAttribute is true)
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
    {
        var attribute = GetAttribute(type);

        if (_options.Conventions?.IgnoreQueueAttribute is false && !string.IsNullOrWhiteSpace(attribute?.Queue))
            return _snakeCase ? WithSnakeCasing(attribute.Queue) : attribute.Queue;
    
        var assembly = type.Assembly.GetName().Name;
        var message = type.Name;

        var exchange = _options.Conventions?.IgnoreExchangeAttribute is true
            ? _options.Exchange?.Name
            : string.IsNullOrWhiteSpace(attribute?.Exchange)
                ? _options.Exchange?.Name
                : attribute.Exchange;

        var queue = _queueTemplate?.Replace("{{assembly}}", assembly)
            .Replace("{{exchange}}", exchange)
            .Replace("{{message}}", message);

        return _snakeCase ? WithSnakeCasing(queue) : queue;
    }
}
