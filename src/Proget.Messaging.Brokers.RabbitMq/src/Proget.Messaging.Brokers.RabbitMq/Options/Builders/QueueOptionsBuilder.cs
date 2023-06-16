namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

internal sealed class QueueOptionsBuilder : IQueueOptionsBuilder
{
    private readonly QueueOptions _options = new();

    public QueueOptions Build()
        => _options;

    public IQueueOptionsBuilder SetAutoDelete(bool value = true)
    {
        _options.AutoDelete = value;
        return this;
    }

    public IQueueOptionsBuilder SetDeclare(bool value = true)
    {
        _options.Declare = value;
        return this;
    }

    public IQueueOptionsBuilder SetDurable(bool value = true)
    {
        _options.Durable = value;
        return this;
    }

    public IQueueOptionsBuilder SetExclusive(bool value = true)
    {
        _options.Exclusive = value;
        return this;
    }

    public IQueueOptionsBuilder SetTemplate(string? value)
    {
        _options.Template = value;
        return this;
    }
}
  