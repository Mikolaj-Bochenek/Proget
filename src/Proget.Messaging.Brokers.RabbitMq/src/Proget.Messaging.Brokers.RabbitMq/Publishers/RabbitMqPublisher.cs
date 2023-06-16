namespace Proget.Messaging.Brokers.RabbitMq;

internal sealed class RabbitMqPublisher : IBrokerPublisher
{
    private readonly IModel _channel;
    private readonly IConventionsProvider _conventionsProvider;
    private readonly RabbitMqOptions _options;
    private readonly ISerializer _serializer;
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly bool _isExchangeDeclared;

    public RabbitMqPublisher(IChannelFactory channelFactory, IConventionsProvider conventionsProvider, RabbitMqOptions options,
        ISerializer serializer, ILogger<RabbitMqPublisher> logger)
    {
        _channel = channelFactory.Create();
        _conventionsProvider = conventionsProvider;
        _options = options;
        _serializer = serializer;
        _logger = logger;
        _isExchangeDeclared = options.Exchange?.PublisherDeclare ?? false;
    }
    
    public async Task PublishAsync<TMessage>(TMessage message, string? messageId, string? correlationId)
        where TMessage : class, IMessage
    {
        var conventions = _conventionsProvider.Get(message.GetType());
        var payload = _serializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(payload);
        var properties = _channel.CreateBasicProperties();

        SetupMessageProperties();

        if (_options.Logger)
        {
            _logger.LogInformation(string.Join(
                Environment.NewLine,
                "The Message has been publishing:",
                $"messageId: '{properties.MessageId}',",
                $"correlationId: '{properties.CorrelationId}',",
                $"timestamp: '{properties.Timestamp}',",
                $"exchange: '{conventions.Exchange}',",
                $"routing-key: '{conventions.RoutingKey}',",
                $"publishing-by: 'Publisher'.")
            );
        }
        
        if (_options.Ack.PublisherBasicAckEnabled)
        {
            _channel.ConfirmSelect();
            _channel.BasicAcks += (sender, eventArgs) =>
            {
                if (_options.Logger)
                {
                    _logger.LogInformation(string.Join(
                        Environment.NewLine,
                        "The Basic ACK has been returned:",
                        $"messageId: '{properties.MessageId}',",
                        $"correlationId: '{properties.CorrelationId}',",
                        $"deliveryTag: '{eventArgs.DeliveryTag}',",
                        $"multipleACK: '{eventArgs.Multiple}',",
                        $"received-by: 'Publisher'.")
                    );
                }
            };
        }

        if (_options.Ack.PublisherComplexAckEnabled)
        {
            _channel.BasicReturn += (sender, eventArgs) =>
            {
                if (_options.Logger)
                {
                    if (eventArgs.ReplyCode == 200)
                    {
                        _logger.LogInformation(string.Join(
                            Environment.NewLine,
                            "The Complex ACK has been returned:",
                            $"messageId: '{properties.MessageId}',",
                            $"correlationId: '{properties.CorrelationId}',",
                            $"replyCode: '{eventArgs.ReplyCode}',",
                            $"replyText: '{eventArgs.ReplyText}',",
                            $"exchange: '{eventArgs.Exchange}',",
                            $"routing-key: '{eventArgs.RoutingKey}',",
                            $"body: '{eventArgs.Body}',",
                            $"received-by: 'Publisher'.")
                        );
                    }
                    else
                    {
                        _logger.LogError(string.Join(
                            Environment.NewLine,
                            "The Complex ACK has been returned:",
                            $"messageId: '{properties.MessageId}',",
                            $"correlationId: '{properties.CorrelationId}',",
                            $"replyCode: '{eventArgs.ReplyCode}',",
                            $"replyText: '{eventArgs.ReplyText}',",
                            $"exchange: '{eventArgs.Exchange}',",
                            $"routing-key: '{eventArgs.RoutingKey}',",
                            $"body: '{eventArgs.Body}',",
                            $"received-by: 'Publisher'.")
                        );
                    }
                }
            };
        }

        if (_isExchangeDeclared)
        {
            var type = string.IsNullOrWhiteSpace(_options.Exchange?.Type) ? ExchangeType.Topic : _options.Exchange?.Type;
            var durable = _options.Exchange?.Durable ?? false;
            var autoDelete = _options.Exchange?.AutoDelete ?? false;

            _channel.ExchangeDeclare(
                conventions.Exchange,
                type,
                durable,
                autoDelete
            );

            if (_options.Logger)
            {
                _logger.LogInformation(string.Join(
                    Environment.NewLine,
                    "The Exchange has been declared:",
                    $"name: '{conventions.Exchange}',",
                    $"routing-key: '{conventions.RoutingKey}',",
                    $"type: '{type}',",
                    $"durable: '{durable}',",
                    $"autoDelete: '{autoDelete}'",
                    $"declared-by: 'Publisher'.")
                );
            }
        }

        _channel.BasicPublish(
            exchange: conventions.Exchange,
            routingKey: conventions.RoutingKey,
            mandatory: _options.Ack.PublisherComplexAckEnabled ? true : false,
            basicProperties: properties,
            body: body
        );

        await Task.CompletedTask;

        if (_options.Logger)
        {
            _logger.LogInformation(string.Join(
                Environment.NewLine,
                "The Message has been published:",
                $"messageId: '{properties.MessageId}',",
                $"correlationId: '{properties.CorrelationId}',",
                $"timestamp: '{properties.Timestamp}',",
                $"exchange: '{conventions.Exchange}',",
                $"routing-key: '{conventions.RoutingKey}',",
                $"publishing-by: 'Publisher'.")
            );
        }

        void SetupMessageProperties()
        {
            properties.Persistent = _options.MessagePersisted;
            properties.MessageId = string.IsNullOrWhiteSpace(messageId)
                ? Guid.NewGuid().ToString("N")
                : messageId;
            properties.CorrelationId = string.IsNullOrWhiteSpace(correlationId)
                ? Guid.NewGuid().ToString("N")
                : correlationId;
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            properties.Headers = new Dictionary<string, object>();
        }
    }
}