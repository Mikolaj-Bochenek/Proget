namespace  Proget.Messaging.Outbox.Configurators;

public interface IOutboxConfigurator
{
    public OutboxOptions Options { get; }
    public IProgetBuilder Builder { get; }
}
