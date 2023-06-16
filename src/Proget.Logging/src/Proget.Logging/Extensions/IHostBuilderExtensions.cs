using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Proget.Extensions;
using Proget.Options;
using Serilog;
using Proget.Logging.Constants;
using Proget.Logging.Options;
using Proget.Logging.Services;

namespace Proget.Logging.Extensions;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, Action<HostBuilderContext, LoggerConfiguration>? configure = null,
        string loggerSectionName = SettingConstants.LoggerSection, string appSectionName = SettingConstants.AppSection)
            => hostBuilder
                .ConfigureServices(services => services.AddSingleton<ILoggingService, LoggingService>())
                .UseSerilog((context, loggerConfiguration) =>
                {
                    var loggerOptions = context.Configuration.GetOptions<LoggerOptions>(loggerSectionName);
                    var progetOptions = context.Configuration.GetOptions<ProgetOptions>(appSectionName);

                    Proget.Logging.Mapper.Mapper.MapOptions(loggerOptions, progetOptions, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
                    configure?.Invoke(context, loggerConfiguration);
                });
}
