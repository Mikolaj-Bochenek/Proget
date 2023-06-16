using Microsoft.AspNetCore.Hosting;
using Serilog;
using Proget.Logging.Constants;
using Proget.Logging.Services;
using Microsoft.Extensions.DependencyInjection;
using Proget.Logging.Options;
using Proget.Options;
using Proget.Extensions;

namespace Proget.Logging.Extensions;

public static class IWebHostBuilderExtensions
{
    public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, Action<WebHostBuilderContext, LoggerConfiguration>? configure = null,
        string loggerSectionName = SettingConstants.LoggerSection, string appSectionName = SettingConstants.AppSection)
            => webHostBuilder
                .ConfigureServices(services => services.AddSingleton<ILoggingService, LoggingService>())
                .UseSerilog((context, loggerConfiguration) =>
                {
                    var loggerOptions = context.Configuration.GetOptions<LoggerOptions>(loggerSectionName);
                    var progetOptions = context.Configuration.GetOptions<ProgetOptions>(appSectionName);

                    Proget.Logging.Mapper.Mapper.MapOptions(loggerOptions, progetOptions, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
                    configure?.Invoke(context, loggerConfiguration);
                });
}
