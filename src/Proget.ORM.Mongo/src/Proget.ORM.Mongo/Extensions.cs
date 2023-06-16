namespace Proget.ORM.Mongo;

public static class Extensions
{
    private const string Section = "orm:mongo";

    public static IStorageConfigurator AddMongo(this IStorageConfigurator configurator,
        Func<IMongoDbOptionsBuilder, IMongoDbOptionsBuilder>? optionsBuilder = null, string section = Section)
    {
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<
            MongoDbOptions, MongoDbOptionsBuilder, IMongoDbOptionsBuilder
        >(section ?? Section, optionsBuilder);

        builder.Services.AddSingleton<IMongoClient>(new MongoClient(options.ConnectionString));

        builder.Services.AddTransient(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.Database);
        });

        return configurator;
    }

    public static IStorageConfigurator AddMongoRepository<TEntity, TIdentifier>(this IStorageConfigurator configurator, string collectionName)
        where TEntity : IIdentifier<TIdentifier>
    {
        var builder = configurator.Builder;

        builder.Services.AddTransient<IRepository<TEntity, TIdentifier>>(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return new MongoRepository<TEntity, TIdentifier>(database, collectionName);
        });

        return configurator;
    }
}
