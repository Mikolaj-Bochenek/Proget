namespace Proget.Assets;

public static class ProgetExtenions
{
    private const string Section = "assets";
    
    public static IProgetBuilder AddAssetsManager(this IProgetBuilder builder,
        Func<IAssetsConfigurator, IAssetsConfigurator> configure, string section = Section)
    {
        if (string.IsNullOrWhiteSpace(section))
            section = Section;
        
        var options = builder.GetOptions<AssetsOptions>(section);
        builder.Services.AddSingleton(options);

        var configurator = new AssetsConfigurator(options, builder);
        configure(configurator);

        builder.Services.AddSingleton<IAssetsService, AssetsService>();

        return builder;
    }
}
