namespace Proget.Messaging.Email.SendGrid.Publishers;

internal sealed class SendGridPublisher : ISendGridPublisher
{
    private readonly EmailOptions _emailOptions;
    private readonly SendGridOptions _sendGridOptions;

    public SendGridPublisher(EmailOptions emailOptions, SendGridOptions sendGridOptions)
        => (_emailOptions, _sendGridOptions) = (emailOptions, sendGridOptions);

    public async Task PublishAsync(EmailMessage message, string? messageId)
    {
        var client = new SendGridClient(_sendGridOptions.ApiKey);
        var from = new EmailAddress(_emailOptions.SenderAddress, _emailOptions.SenderName);
        var to = new EmailAddress(message.EmailRecipient);

        var subject = message.EmailSubject;
        
        var textContent = message.IsEmailBodyHtml ?
                string.Empty : message.EmailBody;

        var htmlContent = message.IsEmailBodyHtml ?
                message.EmailBody : string.Empty;

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, textContent, htmlContent);

        var response = await client.SendEmailAsync(sendGridMessage);

        if (!response.IsSuccessStatusCode)
            throw new SendGridEmailCannotBeSendException();
    }
}