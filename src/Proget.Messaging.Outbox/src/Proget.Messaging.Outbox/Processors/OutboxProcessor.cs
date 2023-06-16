namespace Proget.Messaging.Outbox.Processors;

internal sealed class OutboxProcessor : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly OutboxOptions _options;
    private readonly IMessagePublisher _publisher;
    private readonly ILogger<OutboxProcessor> _logger;
    private readonly TimeSpan _interval;
    private readonly OutboxType _type;
    private Timer _timer = default!;

    public OutboxProcessor(IServiceProvider serviceProvider, IMessagePublisher publisher, OutboxOptions options,
        ILogger<OutboxProcessor> logger)
    {
        if (options.IntervalMilliseconds <= 0)
            throw new Exception($"Invalid outbox interval: {options.IntervalMilliseconds} ms.");
        
        _interval = TimeSpan.FromMilliseconds(options.IntervalMilliseconds);
        _serviceProvider = serviceProvider;
        _publisher = publisher;
        _options = options;
        _logger = logger;
        _type = OutboxType.Sequential;

        if (!string.IsNullOrWhiteSpace(options.Type))
        {
            if (!Enum.TryParse<OutboxType>(options.Type, true, out var outboxType))
                throw new ArgumentException($"Invalid outbox type: '{_type}', " + $"valid types: '{OutboxType.Sequential}', '{OutboxType.Parallel}'.");

            _type = outboxType;
        }

        if (options.Enabled)
        {
            _logger.LogInformation($"Outbox is enabled, type: '{_type}', message processing every {options.IntervalMilliseconds} ms.");
            return;
        }

        _logger.LogInformation("Outbox is disabled.");
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (!_options.Enabled)
            return Task.CompletedTask;

        _timer = new Timer(SendOutboxMessages, null, TimeSpan.Zero, _interval);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (!_options.Enabled)
            return Task.CompletedTask;

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private void SendOutboxMessages(object? state)
        => _ = SendOutboxMessagesAsync();

    private async Task SendOutboxMessagesAsync()
    {
        var jobId = Guid.NewGuid().ToString("N");

        _logger.LogInformation($"Started processing outbox messages... [job id: '{jobId}']");

        var outbox = _serviceProvider.GetRequiredService<IOutboxRepository>();
        var messages = await outbox.GetUnsentAsync();

        _logger.LogInformation($"Found {messages.Count} unsent messages in outbox [job id: '{jobId}'].");

        if (!messages.Any())
        {
            _logger.LogInformation($"No messages to be processed in outbox [job id: '{jobId}'].");
            return;
        }

        foreach (var message in messages.OrderBy(m => m.SentAt))
        {
            await _publisher.PublishAsync(message, message.Id);
            if (_type == OutboxType.Sequential)
                await outbox.ProcessAsync(message);
        }

        if (_type == OutboxType.Parallel)
            await outbox.ProcessAsync(messages);


        _logger.LogInformation($"Processed {messages.Count} outbox messages. [job id: '{jobId}'].");
    }

    private enum OutboxType
    {
        Sequential,
        Parallel
    }
}
