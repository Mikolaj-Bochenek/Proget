namespace Proget.Messaging.Email.Exceptions;

internal class RecipientAddressIsNullException : Exception
{
    private static string? message = "Email message recipient is null";
    public RecipientAddressIsNullException() : base(message) { }
}
