namespace Proget.ORM.Configurators;

internal sealed class StorageConfigurator : IStorageConfigurator
{
    public IProgetBuilder Builder { get; }
    public StorageOptions Options { get; }

    public StorageConfigurator(IProgetBuilder builder, StorageOptions options)
    {
        Builder = builder;
        Options = options;
    }

}
