namespace Proget.Messaging;

public static class ProgetExtensions
{
    private const string Section = "messaging";

    public static IMessageSubscriber UseMessaging(this IApplicationBuilder app)
        => new MessageSubscriber(app.ApplicationServices.GetRequiredService<SubscriptionsChannel>());

    public static IProgetBuilder AddMessaging(this IProgetBuilder builder, Func<IMessagingConfigurator, IMessagingConfigurator> configure,
        string section = Section)
    {
        if (string.IsNullOrWhiteSpace(section))
            section = Section;
        
        var messagingOptions = builder.GetOptions<MessagingOptions>(section);
        builder.Services.AddSingleton(messagingOptions);
        
        var configurator = new MessagingConfigurator(builder, messagingOptions);
        configure(configurator);

        builder.Services.AddSingleton<ISerializer, NewtonsoftJsonSerializer>();

        builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
        builder.Services.AddSingleton<IMessageSubscriber, MessageSubscriber>();

        builder.Services.AddSingleton<SubscriptionsChannel>();
        builder.Services.AddHostedService<SubscriptionsChannelProcessor>();

        return builder;
    }
}