namespace Proget.Cron.Exceptions;

internal class RecipientNumberIsNullOrEmptyException : System.Exception
{
    private static string? message = "Recipient number in sms message is null or empty";
    public RecipientNumberIsNullOrEmptyException() : base(message) { }
}
