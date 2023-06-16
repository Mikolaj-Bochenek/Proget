namespace Proget.ORM.EntityFramework.Builders;

internal sealed class EFOptionsBuilder<TContext> : IEFOptionsBuilder<TContext> where TContext : DbContext
{
    private readonly EntityFrameworkOptions _options = new EntityFrameworkOptions();

    public EntityFrameworkOptions Build()
        => _options;

    public IEFOptionsBuilder<TContext> WithInitializer(bool init)
    {
        _options.Init = init;
        return this;
    }

    public IEFOptionsBuilder<TContext> WithRepositoriesRegister(bool registerRepositories)
    {
        _options.RegisterRepositories = registerRepositories;
        return this;
    }

    public IEFOptionsBuilder<TContext> WithConnectionString(string connectionString)
    {
        _options.ConnectionString = connectionString;
        return this;
    }

    public IEFOptionsBuilder<TContext> WithProvider(DatabaseProviders provider)
    {
        _options.DatabaseProvider = provider;
        return this;
    }

    public IEFOptionsBuilder<TContext> WithGenericRepositoryType(Type genericRepositoryType)
    {
        _options.GenericRepositoryType = genericRepositoryType;
        return this;
    }

    public IEFOptionsBuilder<TContext> WithSeeder(Type seederType)
    {
        if (seederType is not null)
        {
            _options.Seed = true;
            _options.SeederType = seederType;
        }

        return this;
    }
}
