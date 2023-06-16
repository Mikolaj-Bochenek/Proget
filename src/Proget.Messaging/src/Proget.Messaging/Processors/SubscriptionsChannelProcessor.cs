namespace Proget.Messaging.Processors;

internal sealed class SubscriptionsChannelProcessor : BackgroundService
{
    private readonly SubscriptionsChannel _subscriptionsChannel;
    private readonly MessagingOptions _messagingOptions;
    private readonly IServiceProvider _serviceProvider;

    public SubscriptionsChannelProcessor(SubscriptionsChannel subscriptionsChannel, MessagingOptions messagingOptions,
        IServiceProvider serviceProvider)
    {
        _subscriptionsChannel = subscriptionsChannel;
        _messagingOptions = messagingOptions;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var subscription in _subscriptionsChannel.Reader.ReadAllAsync(stoppingToken))
            Subscribe(subscription);
    }

    private void Subscribe(IMessageSubscription messageSubscription)
    {
        if (_messagingOptions.BrokerEnabled)
        {
            var brokerSubscriber = _serviceProvider.GetRequiredService<IBrokerSubscriber>();
            brokerSubscriber.Subscribe(messageSubscription);
        }

        if (_messagingOptions.InMemoryEnabled)
        {
            var channelSubscriber = _serviceProvider.GetRequiredService<IInMemorySubscriber>();
            channelSubscriber.Subscribe(messageSubscription);
        }
    }
}
