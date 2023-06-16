namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

internal sealed class AckOptionsBuilder : IAckOptionsBuilder
{
    private readonly AckOptions _options = new();

    public AckOptions Build()
        => _options;

    public IAckOptionsBuilder SetMultipleAck(bool value = true)
    {
        _options.MultipleAck = value;
        return this;
    }

    public IAckOptionsBuilder SetMultipleNack(bool value = true)
    {
        _options.MultipleNack = value;
        return this;
    }

    public IAckOptionsBuilder SetRequeueRejected(bool value = true)
    {
        _options.RequeueRejected = value;
        return this;
    }

    public IAckOptionsBuilder SetPublisherBasicAck(bool value = true)
    {
        _options.PublisherBasicAckEnabled = value;
        return this;
    }

    public IAckOptionsBuilder SetPublisherComplexAck(bool value = true)
    {
        _options.PublisherComplexAckEnabled = value;
        return this;
    }
}
  