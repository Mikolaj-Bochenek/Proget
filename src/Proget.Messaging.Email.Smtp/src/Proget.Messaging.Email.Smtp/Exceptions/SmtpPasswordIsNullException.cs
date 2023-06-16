namespace Proget.Messaging.Email.Smtp.Exceptions;

internal class SmtpPasswordIsNullException : Exception
{
    private static string? message = "Smtp password is null";
    public SmtpPasswordIsNullException() : base(message) { }
}
