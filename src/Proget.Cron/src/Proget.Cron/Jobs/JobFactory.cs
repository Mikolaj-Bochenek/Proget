namespace Proget.Cron.Jobs;

internal class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public JobFactory(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        => (IJob)_serviceProvider.GetRequiredService(bundle.JobDetail.JobType);

    public void ReturnJob(IJob job)
    {
        if (job is IDisposable disposableJob)
            disposableJob.Dispose();
    }
}
