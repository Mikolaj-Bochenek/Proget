namespace Proget.Messaging.Publishers;

internal sealed class MessagePublisher : IMessagePublisher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly MessagingOptions _options;

    public MessagePublisher(IServiceProvider serviceProvider, MessagingOptions messagingOptions)
    {
        _serviceProvider = serviceProvider;
        _options = messagingOptions;
    }

    public async Task PublishAsync<TMessage>(TMessage message, string? messageId)
        where TMessage : class, IMessage
    {
        if (_options.BrokerEnabled)
        {
            var brokerPublisher = _serviceProvider.GetRequiredService<IBrokerPublisher>();
            await brokerPublisher.PublishAsync<TMessage>(message, messageId);
        }

        if (_options.InMemoryEnabled)
        {
            var inMemoryPublisher = _serviceProvider.GetRequiredService<IInMemoryPublisher>();
            await inMemoryPublisher.PublishAsync<TMessage>(message, messageId);
        }

        if (_options.NotificationEnabled)
        {
            var notificationPublisher = _serviceProvider.GetRequiredService<INotificationPublisher>();
            await notificationPublisher.PublishAsync<TMessage>(message, messageId);
        }

        if (_options.MailingEnabled)
        {
            var emailPublisher = _serviceProvider.GetRequiredService<IEmailPublisher>();
            await emailPublisher.PublishAsync<TMessage>(message, messageId);
        }

        if (_options.SmsEnabled)
        {
            var smsPublisher = _serviceProvider.GetRequiredService<ISmsPublisher>();
            await smsPublisher.PublishAsync<TMessage>(message, messageId);
        }
    }
}