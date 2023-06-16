namespace Proget.Messaging.Email.SendGrid.Exceptions;

internal class SendGridEmailCannotBeSendException : Exception
{
    private static string? message = "SendGrid email cannot be send";
    public SendGridEmailCannotBeSendException() : base(message) { }
}
