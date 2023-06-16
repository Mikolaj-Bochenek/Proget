namespace Proget.Messaging.Email.Models;

public sealed class EmailMessage : IMessage
{
    public string? EmailRecipient { get; set; }
    public string? EmailSubject { get; set; }
    public string? EmailBody { get; set; }
    public bool IsEmailBodyHtml { get; set; }

    public EmailMessage(string recipient, string emailSubject, string emailBody, bool isEmailBodyHtml)
    {
        EmailRecipient = recipient;
        EmailSubject = emailSubject;
        EmailBody = emailBody;
        IsEmailBodyHtml = isEmailBodyHtml;
    }
}
