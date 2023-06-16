namespace ModularRecipient.API.Extensions;

public static class ModularRecipientModuleExtionsions
{
    public static IProgetBuilder AddModularRecipientModule(this IProgetBuilder builder, IConfiguration configuration)
        => builder
            .AddCore(configuration);
}