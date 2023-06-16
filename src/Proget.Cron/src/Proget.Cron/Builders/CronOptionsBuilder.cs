namespace Proget.Cron.Builders;

internal sealed class CronOptionsBuilder : ICronOptionsBuilder
{
    private readonly CronOptions _options = new CronOptions();

    public CronOptions Build()
        => _options;

    public ICronOptionsBuilder WithName(string? name)
    {
        _options.Name = name;
        return this;
    }

    public ICronOptionsBuilder WithExpression(string expression)
    {
        _options.Expression = expression;
        return this;
    }
}
