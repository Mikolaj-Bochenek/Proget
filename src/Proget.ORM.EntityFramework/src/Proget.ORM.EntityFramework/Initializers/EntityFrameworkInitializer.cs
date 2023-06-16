namespace Proget.ORM.EntityFramework.Initializers;

internal sealed class EntityFrameworkInitializer<TContext> : IHostedService where TContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;
    private readonly bool _withSeeder;

    public EntityFrameworkInitializer(IServiceProvider serviceProvider, bool withSeeder = true)
        => (_serviceProvider, _withSeeder) = (serviceProvider, withSeeder);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();

        if (dbContext is not null)
        {
            await dbContext.Database.MigrateAsync();

            var seeder = scope.ServiceProvider.GetService<IDbContextSeeder<TContext>>();

            if (seeder is not null && _withSeeder)
                await seeder.SeedAsync(dbContext);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
}
