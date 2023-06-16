namespace Proget.Messaging.Outbox.Configurators;

public record OutboxConfigurator(OutboxOptions Options, IProgetBuilder Builder) : IOutboxConfigurator;
