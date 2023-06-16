namespace Proget.Assets.Google.Extensions;

public static class AssetsConfiguratorExtensions
{
    private const string Section = "assets:google";
    public static IAssetsConfigurator AddGoogle(this IAssetsConfigurator configurator,
        Func<IGoogleOptionsBuilder, IGoogleOptionsBuilder>? optionsBuilder = null, string section = Section)
    {
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<GoogleOptions, GoogleOptionsBuilder, IGoogleOptionsBuilder>(section ?? Section, optionsBuilder);

        builder.Services.AddSingleton<IGoogleService, GoogleService>();

        configurator.Options.GoogleEnabled = true;

        return configurator;
    }
}
