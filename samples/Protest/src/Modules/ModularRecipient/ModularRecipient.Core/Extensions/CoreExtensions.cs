namespace ModularRecipient.Core.Extensions;

public static class CoreExtensions
{
    public static IProgetBuilder AddCore(this IProgetBuilder builder, IConfiguration configuration)
        => builder.AddMssql(configuration);
}
