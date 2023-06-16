namespace Proget.Messaging.Email.Exceptions;

internal class EmailMessageIsNullException : Exception
{
    private static string? message = "Email message is null";
    public EmailMessageIsNullException() : base(message) { }
}
