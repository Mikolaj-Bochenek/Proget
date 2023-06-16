namespace Proget.CQRS.Logging.Templates;

public sealed class LogTemplate
{
    public string? Before { get; set; }
    public string? After { get; set; }
    public IReadOnlyDictionary<Type, string>? Errors { get; set; }

    public string? GetExceptionTemplate(Exception exception)
    {
        var exceptionType = exception.GetType();

        if (Errors is null)
            return null;

        return Errors.TryGetValue(exceptionType, out var template) ? template : null;
    }
}
