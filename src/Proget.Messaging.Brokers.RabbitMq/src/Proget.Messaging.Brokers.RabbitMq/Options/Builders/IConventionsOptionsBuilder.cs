namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public interface IConventionsOptionsBuilder
{
    ConventionsOptions Build();
    IConventionsOptionsBuilder IgnoreExchangeAttachment(bool value = true);
    IConventionsOptionsBuilder IgnoreRoutingKeyAttachment(bool value = true);
    IConventionsOptionsBuilder IgnoreQueueAttachment(bool value = true);
    IConventionsOptionsBuilder SetCasing(string? value);
}