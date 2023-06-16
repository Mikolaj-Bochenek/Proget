namespace Proget.Cron.Exceptions;

internal class SmsMessageContentIsNullOrEmptyException : System.Exception
{
    private static string? message = "Sms message content is null or empty";
    public SmsMessageContentIsNullOrEmptyException() : base(message) { }
}
