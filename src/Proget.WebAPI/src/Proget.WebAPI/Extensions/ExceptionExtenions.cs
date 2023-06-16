namespace Proget.WebAPI.Extensions;

public static class ExceptionExtenions
{
    public static IProgetBuilder AddErrorHandler<TMapper>(this IProgetBuilder builder)
        where TMapper : class, IExceptionMapper
    {
        builder.Services.AddTransient<ErrorHandlerMiddleware>();
        builder.Services.AddSingleton<IExceptionMapper, TMapper>();

        return builder;
    }

    public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ErrorHandlerMiddleware>();
}
