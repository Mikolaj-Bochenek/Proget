namespace Proget.Auth.Exceptions;

internal sealed class UnauthorizedActionResult : IActionResult
{
    private readonly UnauthorizedException _exception;

    public UnauthorizedActionResult(UnauthorizedException exception)
        => _exception = exception;

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(_exception)
        {
            StatusCode = (int)_exception.StatusCode
        };

        await objectResult.ExecuteResultAsync(context);
    }
}
