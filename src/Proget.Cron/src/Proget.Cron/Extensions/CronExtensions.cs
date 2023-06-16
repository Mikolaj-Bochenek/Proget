namespace Proget.Cron.Extensions;

public static class CronExtensions
{
    private const string Section = "cron";

    public static IProgetBuilder AddCron<TJob>(this IProgetBuilder builder,
        Func<ICronOptionsBuilder, ICronOptionsBuilder>? optionsBuilder = null, string section = Section) where TJob : class, IProgetJob
    {
        if (section.IsNullOrEmpty())
            section = Section;

        var options = builder.ConfigureOptions<CronOptions, CronOptionsBuilder, ICronOptionsBuilder>(section ?? Section, optionsBuilder);

        if (options.Expression!.IsNullOrEmpty())
            throw new CronExpressionIsNullOrEmptyException();

        builder.Services.AddSingleton<IJobFactory, JobFactory>();
        builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
        builder.Services.AddSingleton<TJob>();

        builder.Services.AddSingleton<IQuartzProvider, QuartzProvider>();

        builder.Services.AddHostedService<HostedService<TJob>>();

        return builder;
    }
}
