namespace Proget.Auth.Middleware;

public class AccessTokenValidatorMiddleware : IMiddleware
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IEnumerable<string> _endpoints;

    public AccessTokenValidatorMiddleware(IAccessTokenService accessTokenService, JWTOptions options)
    {
        _accessTokenService = accessTokenService;
        _endpoints = options.AllowAnonymousEndpoints ?? Enumerable.Empty<string>();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authorizationHeader = context.Request.Headers["authorization"].ToString();

        if (authorizationHeader is not null && !authorizationHeader.StartsWith("Bearer "))
        {
            context.Request.Headers["authorization"] = $"Bearer {authorizationHeader}";
        }

        var path = context.Request.Path.HasValue ? context.Request.Path.Value : string.Empty;

        try
        {
            if (_endpoints.Contains(path))
            {
                await next(context);
                return;
            }

            if (await _accessTokenService.IsCurrentTokenActive())
            {
                _accessTokenService.AddContextClaims();
                await next(context);
                return;
            }

            await next(context);

            // throw new UnauthorizedException();
        }
        catch(Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            var result = new UnauthorizedResult(exception.Message, context.Response.StatusCode);
            var responseObject = JsonSerializer.Serialize(result);

            await context.Response.WriteAsync(responseObject);
        }
    }
}
