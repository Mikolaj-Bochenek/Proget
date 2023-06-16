namespace Proget.ORM.Configurators;

public interface IStorageConfigurator
{
    public IProgetBuilder Builder { get; }
    public StorageOptions Options { get; }
}
