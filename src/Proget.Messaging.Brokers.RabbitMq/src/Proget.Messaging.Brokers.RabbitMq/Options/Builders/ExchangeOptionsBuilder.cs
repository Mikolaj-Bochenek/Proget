namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

internal sealed class ExchangeOptionsBuilder : IExchangeOptionsBuilder
{
    private readonly ExchangeOptions _exchangeOptions = new();

    public ExchangeOptions Build()
        => _exchangeOptions;

    public IExchangeOptionsBuilder SetName(string? value)
    {
        _exchangeOptions.Name = value;
        return this;
    }

    public IExchangeOptionsBuilder SetType(string? value)
    {
        _exchangeOptions.Type = value switch
        {
            _ when string.Equals(ExchangeType.Direct, value, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Direct,
            _ when string.Equals(ExchangeType.Fanout, value, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Fanout,
            _ when string.Equals(ExchangeType.Headers, value, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Headers,
            _ when string.Equals(ExchangeType.Topic, value, StringComparison.InvariantCultureIgnoreCase) => ExchangeType.Topic,
            _ => throw new Exception("The type of exchange has to be one of these: ['direct', 'fanout', 'headers', 'topic']")
        };

        return this;
    }

    public IExchangeOptionsBuilder SetAutoDelete(bool value = true)
    {
        _exchangeOptions.AutoDelete = value;
        return this;
    }


    public IExchangeOptionsBuilder SetPublisherDeclare(bool value = true)
    {
        _exchangeOptions.PublisherDeclare = value;
        return this;
    }

    public IExchangeOptionsBuilder SetSubscriberDeclare(bool value = true)
    {
        _exchangeOptions.SubscriberDeclare = value;
        return this;
    }

    public IExchangeOptionsBuilder SetDurable(bool value = true)
    {
        _exchangeOptions.Durable = value;
        return this;
    }
}
