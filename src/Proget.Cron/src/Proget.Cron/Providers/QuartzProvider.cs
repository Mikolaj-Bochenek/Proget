namespace Proget.Cron.Providers;

internal sealed class QuartzProvider : IQuartzProvider
{
    private const string DefaultExpression = "0 0 0 ? * * *";
    private readonly string _id = Guid.NewGuid().ToString("N");
    private readonly CronOptions _cronOptions;

    public QuartzProvider(CronOptions cronOptions)
        => _cronOptions = cronOptions;

    public ITrigger CreateTrigger()
        => TriggerBuilder
            .Create()
            .WithIdentity(_id)
            .WithCronSchedule(_cronOptions.Expression ?? DefaultExpression)
            .Build();

    public IJobDetail CreateJob<TJob>()
        => JobBuilder
            .Create(typeof(TJob))
            .WithIdentity(_id)
            .WithDescription($"{_cronOptions.Name}")
            .Build();
}
