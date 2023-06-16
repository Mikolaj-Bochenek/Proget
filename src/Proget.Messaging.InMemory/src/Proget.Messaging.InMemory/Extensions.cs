namespace Proget.Messaging.InMemory;

public static class Extensions
{
    private const string Section = "messaging:inmemory";

    public static IMessagingConfigurator AddInMemory(this IMessagingConfigurator configurator,
        Func<IInMemoryOptionsBuilder, IInMemoryOptionsBuilder>? optionsBuilder = null, bool enabled = true, string section = Section)
    {
        var builder = configurator.Builder;

        builder.ConfigureOptions<InMemoryOptions, InMemoryOptionsBuilder, IInMemoryOptionsBuilder>(section ?? Section, optionsBuilder);
        
        builder.Services.AddSingleton<IInMemoryPublisher, InMemoryPublisher>();
        builder.Services.AddSingleton<IInMemorySubscriber, InMemorySubscriber>();

        builder.Services.AddTransient<IChannelDispatcher, ChannelDispatcher>();
        builder.Services.AddSingleton<IChannelAccessor, ChannelAccessor>();

        builder.Services.AddSingleton<IConventionsBuilder, InMemoryConventionsBuilder>();
        builder.Services.AddSingleton<IConventionsProvider, InMemoryConventionsProvider>();

        builder.Services.AddSingleton<IAsyncEventingConsumer, AsyncEventingConsumer>();
        builder.Services.AddHostedService<EventAssignerProcessor>();

        configurator.Options.InMemoryEnabled = enabled;

        return configurator;
    }
}
