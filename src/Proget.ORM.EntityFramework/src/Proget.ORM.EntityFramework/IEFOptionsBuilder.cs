namespace Proget.ORM.EntityFramework;

public interface IEFOptionsBuilder<TContext> : IProgetOptionsBuilder<EntityFrameworkOptions> where TContext : DbContext
{
    IEFOptionsBuilder<TContext> WithInitializer(bool init = true);
    IEFOptionsBuilder<TContext> WithSeeder(Type seederType);
    IEFOptionsBuilder<TContext> WithProvider(DatabaseProviders provider);
    IEFOptionsBuilder<TContext> WithGenericRepositoryType(Type genericRepositoryType);
    IEFOptionsBuilder<TContext> WithRepositoriesRegister(bool registerRepositories = true);
    IEFOptionsBuilder<TContext> WithConnectionString(string connectionString);
}
