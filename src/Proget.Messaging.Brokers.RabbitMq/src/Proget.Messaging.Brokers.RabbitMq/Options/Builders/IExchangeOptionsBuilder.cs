namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public interface IExchangeOptionsBuilder
{
    ExchangeOptions Build();
    IExchangeOptionsBuilder SetName(string? value);
    IExchangeOptionsBuilder SetType(string? value);
    IExchangeOptionsBuilder SetPublisherDeclare(bool value = true);
    IExchangeOptionsBuilder SetSubscriberDeclare(bool value = true);
    IExchangeOptionsBuilder SetDurable(bool value = true);
    IExchangeOptionsBuilder SetAutoDelete(bool value = true);
}
