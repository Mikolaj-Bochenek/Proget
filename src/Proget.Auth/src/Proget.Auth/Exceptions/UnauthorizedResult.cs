namespace Proget.Auth.Exceptions;

internal sealed class UnauthorizedResult
{
    public string Message { get; }
    public int StatusCode { get; }
    public UnauthorizedResult(string message, int statusCode)
        => (Message, StatusCode) = (message, statusCode);
}
