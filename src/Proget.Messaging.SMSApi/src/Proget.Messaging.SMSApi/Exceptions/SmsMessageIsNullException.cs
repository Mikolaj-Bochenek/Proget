namespace Proget.Cron.Exceptions;

internal class SmsMessageIsNullException : System.Exception
{
    private static string? message = "Sms message is null";
    public SmsMessageIsNullException() : base(message) { }
}
