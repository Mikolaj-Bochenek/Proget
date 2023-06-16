namespace Proget.Messaging.InMemory.Publishers;

internal sealed class InMemoryPublisher : IInMemoryPublisher
{
    private readonly IChannelDispatcher _dispatcher;
    private readonly InMemoryOptions _options;
    private readonly IConventionsProvider _conventionsProvider;
    private readonly ISerializer _serializer;
    private readonly ILogger<InMemoryPublisher> _logger;

    public InMemoryPublisher(IChannelDispatcher dispatcher, InMemoryOptions options, IConventionsProvider conventionsProvider,
        ISerializer serializer, ILogger<InMemoryPublisher> logger)
    {
        _dispatcher = dispatcher;
        _options = options;
        _conventionsProvider = conventionsProvider;
        _serializer = serializer;
        _logger = logger;
    }

    public async Task PublishAsync<TMessage>(TMessage message, string? messageId)
        where TMessage : class, IMessage
    {
        var conventions = _conventionsProvider.Get(message.GetType());
        var payload = _serializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(payload);

        messageId = string.IsNullOrWhiteSpace(messageId)
            ? Guid.NewGuid().ToString("N")
            : messageId;
        
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var headers = new Dictionary<string, object>();


        if (_options.Logger)
            _logger.LogInformation($"Publishing an in-memory message [id: '{messageId}']" +
                $" to exchange: '{conventions.Exchange}'");
        
        var messageEnvelope = new MessageEnvelope(
            messageId,
            conventions.Exchange,
            conventions.RoutingKey,
            timestamp,
            headers,
            body
        );
        
        await _dispatcher.WriteAsync(messageEnvelope);

        if (_options.Logger)
        {
            _logger.LogInformation(string.Join(
                Environment.NewLine,
                "The in-memory message has been published:",
                $"messageId: '{messageId}',",
                $"timestamp: '{timestamp}',",
                $"exchange: '{conventions.Exchange}'",
                $"routing-key: '{conventions.RoutingKey}',",
                $"payload: '{payload}'",
                $"published-by: 'Publisher'.")
            );
        }

        await Task.CompletedTask;
    }
}
