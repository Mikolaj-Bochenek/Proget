namespace Proget.Messaging.Brokers.RabbitMq.Subscribers;

internal sealed class RabbitMqSubscriber : IBrokerSubscriber
{
    private readonly IModel _channel;
    private readonly ISerializer _serializer;
    private readonly IConventionsProvider _conventionsProvider;
    private readonly IServiceProvider _serviceProvider;
    private readonly RabbitMqOptions _options;
    private readonly QosOptions _qosOptions;
    private readonly QueueOptions _queueOptions;
    private readonly AckOptions _ackOptions;
    private readonly ExchangeOptions _exchangeOptions;
    private readonly ILogger<RabbitMqSubscriber> _logger;

    public RabbitMqSubscriber(IChannelFactory channelFactory, ISerializer serializer, IConventionsProvider conventionsProvider,
        IServiceProvider serviceProvider, RabbitMqOptions options, ILogger<RabbitMqSubscriber> logger)
    {
        _channel = channelFactory.Create();
        _serializer = serializer;
        _conventionsProvider = conventionsProvider;
        _serviceProvider = serviceProvider;
        _options = options;
        _qosOptions = options.Qos;
        _queueOptions = _options.Queue;
        _ackOptions = _options.Ack;
        _exchangeOptions = _options.Exchange;
        _logger = logger;
    }

    public void Subscribe(IMessageSubscription subscription)
    {
        var type = subscription.Type;
        var callback = subscription.Callback;
        var conventions = _conventionsProvider.Get(type);

        // CONVENTIONS
        var exchange = conventions.Exchange;
        var routingKey = conventions.RoutingKey;
        var queue = conventions.Queue;
        var messageType = conventions.Type;

        if (_options.Logger)
        {
            _logger.LogInformation(string.Join(
                Environment.NewLine,
                "The Channel has been declared:",
                $"number: '{_channel.ChannelNumber}',",
                $"exchange: '{exchange}',",
                $"routing-key: '{routingKey}',",
                $"queue: '{queue}',",
                $"declared-by: 'Subscriber'.")
            );
        }
        
        // EXCHANGE
        var declareExchange = _exchangeOptions.SubscriberDeclare;
        var durableExchange = _exchangeOptions.Durable;
        var autoDeleteExchange = _exchangeOptions.AutoDelete;
        var exchangeType = string.IsNullOrWhiteSpace(_exchangeOptions.Type) ? ExchangeType.Topic : _exchangeOptions.Type;
        
        if (declareExchange)
        {
            _channel.ExchangeDeclare(exchange, exchangeType, durableExchange, autoDeleteExchange);

            if (_options.Logger)
            {
                _logger.LogInformation(string.Join(
                    Environment.NewLine,
                    "The Exchange has been declared:",
                    $"name: '{exchange}',",
                    $"routing-key: '{routingKey}',",
                    $"queue: '{queue}',",
                    $"type: '{exchangeType}',",
                    $"durable: '{durableExchange}',",
                    $"autoDelete: '{autoDeleteExchange}'",
                    $"declared-by: 'Subscriber'.")
                );
            }
        }

        // QUEUE
        var declare = _queueOptions.Declare;
        var durable = _queueOptions.Durable;
        var exclusive = _queueOptions.Exclusive;
        var autoDelete = _queueOptions.AutoDelete;
        var prefetchCount = _qosOptions.PrefetchCount < 1 ? (ushort)1 : _qosOptions.PrefetchCount;

        if (declare)
        {
            _channel.QueueDeclare(queue, durable, exclusive, autoDelete);

            if (_options.Logger)
            {
                _logger.LogInformation(string.Join(
                    Environment.NewLine,
                    "The Queue has been declared:",
                    $"name: '{queue}',",
                    $"routing-key: '{routingKey}',",
                    $"exchange: '{exchange}',",
                    $"durable: '{durable}',",
                    $"exclusive: '{exclusive}',",
                    $"autoDelete: '{autoDelete}'",
                    $"declared-by: 'Subscriber'.")
                );
            }
        }

        _channel.QueueBind(queue, exchange, routingKey);
        _channel.BasicQos(_qosOptions.PrefetchSize, prefetchCount, _qosOptions.Global);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            try
            {
                var messageId = eventArgs.BasicProperties.MessageId;
                var correlationId = eventArgs.BasicProperties.CorrelationId;
                var timestamp = eventArgs.BasicProperties.Timestamp.UnixTime;
                var payload = Encoding.UTF8.GetString(eventArgs.Body.Span);
                var message = _serializer.Deserialize(payload, type);

                if (_options.Logger)
                {
                    _logger.LogInformation(string.Join(
                        Environment.NewLine,
                        "The Message has been received:",
                        $"messageId: '{messageId}',",
                        $"correlationId: '{correlationId}',",
                        $"timestamp: '{timestamp}',",
                        $"queue: '{queue}',",
                        $"routing-key: '{routingKey}',",
                        $"exchange: '{exchange}'",
                        $"payload: '{payload}'",
                        $"received-by: 'Subscriber'.")
                    );
                }

                if (message is not null)
                    await callback(_serviceProvider, message);

                _channel.BasicAck(eventArgs.DeliveryTag, _ackOptions.MultipleAck);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                
                if (_ackOptions.MultipleNack)
                    _channel.BasicNack(eventArgs.DeliveryTag, true, _ackOptions.RequeueRejected);
                    
                _channel.BasicReject(eventArgs.DeliveryTag, _ackOptions.RequeueRejected);

                await Task.Yield();
                throw;
            }
        };

        _channel.BasicConsume(conventions.Queue, false, consumer);
    }
}
