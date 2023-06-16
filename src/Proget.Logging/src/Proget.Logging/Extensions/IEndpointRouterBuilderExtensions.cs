using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;

namespace Proget.Logging.Extensions;

public static class IEndpointRouterBuilderExtensions
{
    internal static LoggingLevelSwitch LoggingLevelSwitch = new();

    public static IEndpointConventionBuilder MapLogLevelHandler(this IEndpointRouteBuilder builder,
        string endpointRoute = "~/logging/level")
        => builder.MapPost(endpointRoute, LevelSwitch);

    private static async Task LevelSwitch(HttpContext context)
    {
        var service = context.RequestServices.GetService<ILoggingService>();
        if (service is null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("ILoggingService is not registered. Add UseLogging() to your Program.cs.");
            return;
        }

        var level = context.Request.Query["level"].ToString();

        if (string.IsNullOrEmpty(level))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid value for logging level.");
            return;
        }

        service.SetLoggingLevel(level);

        context.Response.StatusCode = StatusCodes.Status200OK;
    }
}
