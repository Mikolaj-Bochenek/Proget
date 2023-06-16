namespace Proget.Messaging.Email.Smtp.Exceptions;

internal class SmtpMessageIsNullException : Exception
{
    private static string? message = "Smtp message is null";
    public SmtpMessageIsNullException() : base(message) { }
}
