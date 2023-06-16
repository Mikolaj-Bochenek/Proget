namespace Proget.Auth.Extensions;

internal static class DateTimeExtensions
{
    public static long ToTimestamp(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
}
