namespace Proget.Auth.Extensions;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAccessTokenValidator(this IApplicationBuilder app)
        => app
            .UseMiddleware<AccessTokenValidatorMiddleware>()
            .UseAuthorization();

}
