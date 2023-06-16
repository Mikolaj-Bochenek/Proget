namespace Proget.WebAPI.Exceptions;

internal sealed class ErrorHandlerMiddleware : IMiddleware
{
    private readonly IExceptionMapper _exceptionMapper;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(IExceptionMapper exceptionMapper, ILogger<ErrorHandlerMiddleware> logger)
    {
        _exceptionMapper = exceptionMapper;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = _exceptionMapper.Map(exception);
        context.Response.StatusCode = (int)(exceptionResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        var response = exceptionResponse?.Response;
        if (response is null)
        {
            await context.Response.WriteAsync(string.Empty);
            return;
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(response);
    }
}
