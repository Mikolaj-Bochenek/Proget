namespace Proget.Messaging.Email.Exceptions;

internal class SenderAddressIsNullException : Exception
{
    private static string? message = "Email sender address is null";
    public SenderAddressIsNullException() : base(message) { }
}