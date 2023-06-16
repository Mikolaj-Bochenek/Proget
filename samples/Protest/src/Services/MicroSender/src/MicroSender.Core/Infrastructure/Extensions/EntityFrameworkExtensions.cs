namespace MicroSender.Core.Infrastructure.Extensions;

internal static class EntityFrameworkExtensions
{
    public static IProgetBuilder AddMssql(this IProgetBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddScoped<IMicroSenderRepository, MicroSenderRepository>();

        builder.AddEntityFrameworkDbContext<MicroSenderDbContext>(options =>
            options
                .WithConnectionString(configuration["ef:connectionString"])
                .WithInitializer()
                .WithProvider(DatabaseProviders.SqlServer)
                .WithRepositoriesRegister(false));

        return builder;
    }
}