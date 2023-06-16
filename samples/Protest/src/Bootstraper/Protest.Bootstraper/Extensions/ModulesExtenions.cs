namespace Protest.Bootstraper.Extensions;

public static class ModulesExtensions
{
    public static IProgetBuilder AddModules(this IProgetBuilder builder, IConfiguration configuration)
        => builder
            .AddModularRecipientModule(configuration)
            .AddSenderModule(configuration);
    
    public static IApplicationBuilder UseModules(this IApplicationBuilder app)
        => app;
}
