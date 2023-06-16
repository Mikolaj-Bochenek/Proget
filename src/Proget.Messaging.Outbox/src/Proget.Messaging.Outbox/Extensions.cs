namespace Proget.Messaging.Outbox;

public static class Extensions
{
    private const string Section = "messaging:outbox";
    public static IMessagingConfigurator WithOutbox(this IMessagingConfigurator configurator, Action<IOutboxConfigurator>? configure = null,
        Func<IOutboxOptionsBuilder, IOutboxOptionsBuilder>? optionsBuilder = null, bool enabled = true, string section = Section)
    {
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<OutboxOptions, OutboxOptionsBuilder, IOutboxOptionsBuilder>(section ?? Section, optionsBuilder);

        var outboxConfigurator = new OutboxConfigurator(options, builder);
            configure?.Invoke(outboxConfigurator);
            
        builder.Services.AddHostedService<OutboxProcessor>();

        return configurator;
    }
}
