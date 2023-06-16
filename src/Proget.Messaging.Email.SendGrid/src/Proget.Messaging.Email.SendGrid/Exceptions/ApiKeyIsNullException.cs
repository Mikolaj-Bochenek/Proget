namespace Proget.Messaging.Email.SendGrid.Exceptions;

internal class ApiKeyIsNullException : Exception
{
    private static string? message = "SendGrid API key is null";
    public ApiKeyIsNullException() : base(message) { }
}
