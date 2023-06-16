using Proget.Messaging.InMemory.Types;

namespace Proget.Messaging.InMemory.Subscribers;

internal sealed class InMemorySubscriber : IInMemorySubscriber
{
    private readonly IAsyncEventingConsumer _asyncEventingConsumer;
    private readonly ISerializer _serializer;
    private readonly IConventionsProvider _conventionsProvider;
    private readonly IServiceProvider _serviceProvider;
    private readonly InMemoryOptions _options;
    private readonly ILogger<InMemorySubscriber> _logger;

    public InMemorySubscriber(IAsyncEventingConsumer asyncEventingConsumer, ISerializer serializer,
        IConventionsProvider conventionsProvider, IServiceProvider serviceProvider, InMemoryOptions options, ILogger<InMemorySubscriber> logger)
    {
        _asyncEventingConsumer = asyncEventingConsumer;
        _serializer = serializer;
        _conventionsProvider = conventionsProvider;
        _serviceProvider = serviceProvider;
        _options = options;
        _logger = logger;
    }

    public void Subscribe(IMessageSubscription subscription)
    {
        var type = subscription.Type;
        var callback = subscription.Callback;
        var conventions = _conventionsProvider.Get(type);

        _asyncEventingConsumer.Received += async (deliveryEventArgs) =>
        {
            var messageEnvelope = deliveryEventArgs.MessageEnvelope;
            var messageId = messageEnvelope.MessageId;
            var exchange = messageEnvelope.Exchange;
            var routingKey = messageEnvelope.RoutingKey;
            var payload = Encoding.UTF8.GetString(messageEnvelope.Body);
            var message = _serializer.Deserialize(payload, type);

            switch (exchange)
            {
                case ExchangeType.Fanout when message is not null:
                    await callback(_serviceProvider, message);
                    break;
                case ExchangeType.Direct when message is not null && routingKey.Equals(conventions.RoutingKey):
                    await callback(_serviceProvider, message);
                    break;
                case ExchangeType.Headers when message is not null:
                    throw new NotImplementedException();
                case ExchangeType.Topic when message is not null && IsTopicExcpressionMatch():
                    await callback(_serviceProvider, message);
                    break;
                default:
                    throw new Exception("The type of exchange has to be one of these: ['direct', 'fanout', 'headers', 'topic']");
            }

            if (_options.Logger)
            {
                _logger.LogInformation(string.Join(
                    Environment.NewLine,
                    "The in-memory message has been received:",
                    $"messageId: '{messageId}',",
                    $"timestamp: '{messageEnvelope.Timestamp}',",
                    $"exchange: '{exchange}'",
                    $"routing-key: '{routingKey}',",
                    $"payload: '{payload}'",
                    $"received-by: 'Subscriber'.")
                );
            }    

            await Task.CompletedTask;

            bool IsTopicExcpressionMatch()
            {
                var conventionsRoutingKeyWords = conventions?.RoutingKey.Split('.');
                var messageRoutingKeyWords = routingKey?.Split('.');

                if (conventionsRoutingKeyWords is null || messageRoutingKeyWords is null)
                    throw new ArgumentNullException("RoutingKEy cannot be null.");

                if (conventionsRoutingKeyWords.Length != messageRoutingKeyWords.Length)
                    return false;
                
                var matches = conventionsRoutingKeyWords?.Zip(messageRoutingKeyWords, (first, second) => first.Equals(second) || first.Equals("*"));
                var mismatchfound = matches?.Contains(false) ?? true;
                
                return !mismatchfound;
            }
        };
    }
}