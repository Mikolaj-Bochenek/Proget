namespace Proget.Messaging.Email.Smtp.Exceptions;

internal class SmtpHostAddressIsNullException : Exception
{
    private static string? message = "Smtp host address is null";
    public SmtpHostAddressIsNullException() : base(message) { }
}
