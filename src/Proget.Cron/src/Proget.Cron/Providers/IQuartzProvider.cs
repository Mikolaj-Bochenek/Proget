namespace Proget.Cron.Providers;

internal interface IQuartzProvider
{
    ITrigger CreateTrigger();
    IJobDetail CreateJob<TJob>();
}
