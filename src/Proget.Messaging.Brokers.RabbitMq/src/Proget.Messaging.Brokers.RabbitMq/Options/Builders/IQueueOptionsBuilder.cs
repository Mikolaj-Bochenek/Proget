namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public interface IQueueOptionsBuilder
{
    QueueOptions Build();
    IQueueOptionsBuilder SetTemplate(string? value);
    IQueueOptionsBuilder SetExclusive(bool value = true);
    IQueueOptionsBuilder SetDeclare(bool value = true);
    IQueueOptionsBuilder SetDurable(bool value = true);
    IQueueOptionsBuilder SetAutoDelete(bool value = true);
}
  