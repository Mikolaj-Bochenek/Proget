namespace Proget.Assets.Extensions;

public static class PathExtensions
{
    public static string? GetExtensionWithoutDot(string? path)
        => Path.GetExtension(path)?.Replace(".", string.Empty);

    public static string GetFileNameWithDateTimeFilePrefix(string? path)
        => $"{GetDateTimeFilePrefix()}{path}";

    public static string GetFileNameWithDateTimeFileSuffix(string? path)
        => $"{path}{GetDateTimeFileSuffix()}";

    public static string GetFileNameWithDateTimeFilePrefix(string? path, DateTime? dateTime)
        => $"{GetDateTimeFilePrefix(dateTime)}{path}";

    public static string GetFileNameWithDateTimeFileSuffix(string? path, DateTime? dateTime)
        => $"{path}{GetDateTimeFileSuffix(dateTime)}";

    public static string GetDateTimeFilePrefix()
        => $"{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}__";

    public static string GetDateTimeFileSuffix()
        => $"__{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}";

    public static string GetDateTimeFilePrefix(DateTime? dateTime)
        => $"{dateTime?.ToString("yyyyMMddHHmmssfff")}__";

    public static string GetDateTimeFileSuffix(DateTime? dateTime)
        => $"__{dateTime?.ToString("yyyyMMddHHmmssfff")}";

    public static string CombineExtension(string? fileName, string? extension)
        => $"{fileName}.{extension}";

    public static string GetPathWithSlashPrefix(string? path)
        => $"/{path}";

    public static string Combine(string? path1, string? path2)
        => $"{path1}/{path2}";
}
