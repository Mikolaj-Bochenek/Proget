namespace Proget.ORM.EntityFramework.Options;

public class EntityFrameworkOptions
{
    public bool Seed { get; set; } = false;
    public bool Init { get; set; } = false;
    public bool RegisterRepositories { get; set; } = false;
    public string ConnectionString { get; set; } = string.Empty;
    public DatabaseProviders DatabaseProvider { get; set; } = DatabaseProviders.SqlServer;
    public Type? SeederType { get; set; }
    public Type? GenericRepositoryType { get; set; }
}
