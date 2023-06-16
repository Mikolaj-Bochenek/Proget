namespace Proget.Cron;

public interface ICronOptionsBuilder : IProgetOptionsBuilder<CronOptions>
{
    ICronOptionsBuilder WithName(string? name);
    ICronOptionsBuilder WithExpression(string expression);
}
