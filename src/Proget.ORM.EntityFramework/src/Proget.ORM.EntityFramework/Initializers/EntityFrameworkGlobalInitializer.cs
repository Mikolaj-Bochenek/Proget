namespace Proget.ORM.EntityFramework.Initializers;

internal sealed class EntityFrameworkGlobalInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkGlobalInitializer(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dbContextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(a => typeof(DbContext).IsAssignableFrom(a) && !a.IsInterface && a != typeof(DbContext));

        using var scope = _serviceProvider.CreateScope();

        foreach (var dbContextType in dbContextTypes)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService(dbContextType) as DbContext;

            if (dbContext is null)
                continue;

            await dbContext.Database.MigrateAsync();

            var seederType = typeof(IDbContextSeeder<>).MakeGenericType(dbContext.GetType());
            var seeder = scope.ServiceProvider.GetRequiredService(seederType);

            if (seeder is null)
                continue;

            // var x = (IDbContextSeeder<dbContextType>)seeder;

            // await x.SeedAsync(dbContext);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
}
