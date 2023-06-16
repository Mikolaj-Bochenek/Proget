namespace Proget.Messaging.Email.Publishers;

internal sealed class EmailPublisher : IEmailPublisher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly EmailOptions _options;

    public EmailPublisher(IServiceProvider serviceProvider, EmailOptions messagingOptions)
    {
        _serviceProvider = serviceProvider;
        _options = messagingOptions;
    }

    public async Task PublishAsync<TMessage>(TMessage message, string? messageId) where TMessage : class, IMessage
    {
        var emailMessage = message as EmailMessage ??
            throw new EmailMessageIsNullException();

        if (emailMessage.EmailRecipient is null)
            throw new RecipientAddressIsNullException();

        if (_options.SmtpEnabled)
        {
            var smtpPublisher = _serviceProvider.GetRequiredService<ISmtpPublisher>();
            await smtpPublisher.PublishAsync(emailMessage, messageId);
        }

        if (_options.SendGridEnabled)
        {
            var sendGridPublisher = _serviceProvider.GetRequiredService<ISendGridPublisher>();
            await sendGridPublisher.PublishAsync(emailMessage, messageId);
        }
    }
}