namespace ModularRecipient.Core.Infrastructure.Extensions;

internal static class EntityFrameworkExtensions
{
    public static IProgetBuilder AddMssql(this IProgetBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddScoped<IRecipientRepository, RecipientRepository>();

        builder.AddEntityFrameworkDbContext<RecipientDbContext>(options =>
            options
                .WithConnectionString(configuration["ef:connectionString"])
                .WithInitializer()
                .WithProvider(DatabaseProviders.SqlServer)
                .WithRepositoriesRegister(false));

        return builder;
    }
}