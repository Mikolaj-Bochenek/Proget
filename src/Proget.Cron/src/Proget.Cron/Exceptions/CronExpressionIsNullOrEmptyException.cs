namespace Proget.Cron.Exceptions;

internal class CronExpressionIsNullOrEmptyException : Exception
{
    private static string? message = "Cron expression is null or empty";
    public CronExpressionIsNullOrEmptyException() : base(message) { }
}