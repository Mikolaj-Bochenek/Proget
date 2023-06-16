namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

internal sealed class QosOptionsBuilder : IQosOptionsBuilder
{
    private readonly QosOptions _option = new();

    public QosOptions Build()
        => _option;

    public IQosOptionsBuilder SetGlobal(bool value = true)
    {
        _option.Global = value;
        return this;
    }

    public IQosOptionsBuilder SetPrefetchCount(ushort value)
    {
        _option.PrefetchCount = value;
        return this;
    }

    public IQosOptionsBuilder SetPrefetchSize(uint value)
    {
        _option.PrefetchSize = value;
        return this;
    }
}
  