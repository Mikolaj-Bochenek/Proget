namespace MicroSender.Core.Extensions;

public static class CoreExtensions
{
    public static IProgetBuilder AddCore(this IProgetBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddSingleton<IEventToMessageMapper, EventToMessageMapper>();
        builder.Services.AddTransient<IEventProcessor, EventProcessor>();
        builder.Services.AddTransient<IMessageProcessor, MessageProcessor>();

        builder.AddMssql(configuration);

        return  builder;
    }
}
