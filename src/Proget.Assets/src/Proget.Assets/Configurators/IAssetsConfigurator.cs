namespace Proget.Assets.Configurators;

public interface IAssetsConfigurator
{
    public IProgetBuilder Builder { get; }
    public AssetsOptions Options { get; }
}