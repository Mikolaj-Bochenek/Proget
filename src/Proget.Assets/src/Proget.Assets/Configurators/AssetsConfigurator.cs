namespace Proget.Assets.Configurators;

public sealed record AssetsConfigurator(
    AssetsOptions Options,
    IProgetBuilder Builder
) : IAssetsConfigurator
{
}
