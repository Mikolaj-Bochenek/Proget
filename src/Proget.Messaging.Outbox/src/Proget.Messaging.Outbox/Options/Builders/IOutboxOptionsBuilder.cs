namespace Proget.Messaging.Outbox.Options.Builders;

public interface IOutboxOptionsBuilder : IProgetOptionsBuilder<OutboxOptions>
{
    IOutboxOptionsBuilder SetInterval(int value);
    IOutboxOptionsBuilder SetType(string value);
}
