namespace Proget.ORM.Mongo;

public interface IMongoDbOptionsBuilder : IProgetOptionsBuilder<MongoDbOptions>
{
    IMongoDbOptionsBuilder WithConnectionString(string connectionString);
    IMongoDbOptionsBuilder WithDatabase(string database);
}