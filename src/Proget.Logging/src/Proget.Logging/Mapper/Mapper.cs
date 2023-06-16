using Proget.Options;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Proget.Logging.Extensions;
using Proget.Logging.Options;

namespace Proget.Logging.Mapper;

public static class Mapper
{
    internal static LogEventLevel GetLogEventLevel(string? level)
        => Enum.TryParse<LogEventLevel>(level, true, out var logLevel)
            ? logLevel
            : LogEventLevel.Information;

    public static void MapOptions(LoggerOptions loggerOptions, ProgetOptions progetOptions, LoggerConfiguration loggerConfiguration, string environmentName)
    {
        IEndpointRouterBuilderExtensions.LoggingLevelSwitch.MinimumLevel = GetLogEventLevel(loggerOptions.Level);

        loggerConfiguration.Enrich.FromLogContext()
            .MinimumLevel.ControlledBy(IEndpointRouterBuilderExtensions.LoggingLevelSwitch)
            .Enrich.WithProperty("Environment", environmentName)
            .Enrich.WithProperty("Application", progetOptions.Service)
            .Enrich.WithProperty("Instance", progetOptions.Instance)
            .Enrich.WithProperty("Version", progetOptions.Version);

        foreach (var (key, value) in loggerOptions.Tags ?? new Dictionary<string, object>())
        {
            loggerConfiguration.Enrich.WithProperty(key, value);
        }

        foreach (var (key, value) in loggerOptions.MinimumLevelOverrides ?? new Dictionary<string, string>())
        {
            var logLevel = GetLogEventLevel(value);
            loggerConfiguration.MinimumLevel.Override(key, logLevel);
        }

        loggerOptions.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p))));

        loggerOptions.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty(p)));

        Configure(loggerConfiguration, loggerOptions);
    }

    private static void Configure(LoggerConfiguration loggerConfiguration,
        LoggerOptions options)
    {
        var consoleOptions = options.Console ?? new ConsoleOptions();
        var fileOptions = options.File ?? new Options.FileOptions();

        if (consoleOptions.Enabled)
            loggerConfiguration.WriteTo.Console();

        if (fileOptions.Enabled)
        {
            var path = string.IsNullOrWhiteSpace(fileOptions.Path) ? "logs/logs.txt" : fileOptions.Path;

            if (!Enum.TryParse<RollingInterval>(fileOptions.Interval, true, out var interval))
                interval = RollingInterval.Day;

            loggerConfiguration.WriteTo.File(path, rollingInterval: interval);
        }
    }
}
