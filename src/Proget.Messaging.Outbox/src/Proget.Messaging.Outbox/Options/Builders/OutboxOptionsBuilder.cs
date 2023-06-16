namespace Proget.Messaging.Outbox.Options.Builders;

public sealed class OutboxOptionsBuilder : IOutboxOptionsBuilder
{
    private readonly OutboxOptions _options = new();

    public OutboxOptions Build()
        => _options;

    public IOutboxOptionsBuilder SetInterval(int value)
    {
        _options.IntervalMilliseconds = value;
        return this;
    }

    public IOutboxOptionsBuilder SetType(string value)
    {
        _options.Type = value;
        return this;
    }
}
