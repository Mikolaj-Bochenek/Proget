namespace Proget.Messaging.Email.Smtp.Publishers;

internal sealed class SmtpPublisher : ISmtpPublisher
{
    private readonly EmailOptions _emailOptions;
    private readonly SmtpOptions _smtpOptions;

    public SmtpPublisher(EmailOptions emailOptions, SmtpOptions smtpOptions)
        => (_emailOptions, _smtpOptions) = (emailOptions, smtpOptions);

    public async Task PublishAsync(EmailMessage message, string? messageId)
    { 
        var smtpMessage = new MailMessage();

        smtpMessage.To.Add(new MailAddress(message.EmailRecipient!));
        smtpMessage.From = new MailAddress(_emailOptions.SenderAddress!, _emailOptions.SenderName);
        smtpMessage.Subject = message.EmailSubject;
        smtpMessage.Body = message.EmailBody;
        smtpMessage.IsBodyHtml = message.IsEmailBodyHtml;

        using (var smtp = new SmtpClient())
        {
            var credential = new NetworkCredential
            {
                UserName = _emailOptions.SenderAddress,
                Password = _smtpOptions!.Password
            };

            smtp.Credentials = credential;
            smtp.Host = _smtpOptions.Host!;
            smtp.Port = _smtpOptions.Port;
            smtp.EnableSsl = _smtpOptions.SslEnabled;

            await smtp.SendMailAsync(smtpMessage);
        } 
    }
}
