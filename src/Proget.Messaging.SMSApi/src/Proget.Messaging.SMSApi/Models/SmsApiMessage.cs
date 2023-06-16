namespace Proget.Messaging.SMSApi.Models;

public class SmsApiMessage : IMessage
{
    public string? RecipientNumber { get; set; }
    public string? Message { get; set; }

    public SmsApiMessage(string recipientNumber, string message)
    {
        RecipientNumber = recipientNumber;
        Message = message;
    }
}
