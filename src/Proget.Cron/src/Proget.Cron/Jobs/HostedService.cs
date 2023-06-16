namespace Proget.Cron.Jobs;

internal class HostedService<TJob> : IHostedService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private readonly IQuartzProvider _quartzProvider;
    private IScheduler? Scheduler;

    public HostedService(ISchedulerFactory schedulerFactory, IQuartzProvider quartzProvider, IJobFactory jobFactory)
    {
        _schedulerFactory = schedulerFactory;
        _quartzProvider = quartzProvider;
        _jobFactory = jobFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Scheduler = await _schedulerFactory.GetScheduler().ConfigureAwait(true);
        Scheduler.JobFactory = _jobFactory;

        await Scheduler.Start(cancellationToken).ConfigureAwait(true);

        await Scheduler.ScheduleJob(_quartzProvider.CreateJob<TJob>(), _quartzProvider.CreateTrigger(), cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (Scheduler is not null)
            await Scheduler.Shutdown(cancellationToken);
    }
}
