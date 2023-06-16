namespace Sender.API.Extensions;

public static class SenderModuleExtensions
{
    public static IProgetBuilder AddSenderModule(this IProgetBuilder builder, IConfiguration configuration)
        => builder
            .AddCore(configuration);
}
