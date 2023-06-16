using Proget.Builders;
using Proget.Exceptions;
using Proget.Generators;
using Proget.Options;
using static System.Console;

namespace Proget.Extensions;

public static class ProgetExtensions
{
    private const string Section = "proget";

    public static IProgetBuilder AddProget(this IServiceCollection services, IConfiguration configuration,
        string appSectionName = Section)
    {
        var builder = ProgetBuilder.Create(services, configuration);

        var options = builder.GetOptions<ProgetOptions>(appSectionName);
        services.AddSingleton(options);
        
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IGenerator, Generator>();

        if (!options.DisplayBanner || string.IsNullOrWhiteSpace(options.Name))
            return builder;

        var version = options.DisplayVersion ? $" {options.Version}" : string.Empty;

        WriteLine(Figgle.FiggleFonts.Doom.Render($"{options.Name}{version}"));

        return builder;
    }

    public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
        where TModel : new()
    {
        var model = new TModel();
        configuration.GetSection(sectionName).Bind(model);
        return model;
    }
    
    public static TModel GetOptions<TModel>(this IProgetBuilder builder, string settingsSectionName)
        where TModel : new()
        => builder.Configuration.GetOptions<TModel>(settingsSectionName);

    public static TOptions ConfigureOptions<TOptions, TOptionsBuilder, TIOptionsBuilder>(this IProgetBuilder builder, string section,
        Func<TIOptionsBuilder, TIOptionsBuilder>? optionsBuilder = null)
        where TOptions : class, new()
        where TIOptionsBuilder: IProgetOptionsBuilder<TOptions>
        where TOptionsBuilder : TIOptionsBuilder, new()
        
    {
        var settings = builder.GetOptions<TOptions>(section);
        var options = optionsBuilder?.Invoke(new TOptionsBuilder()).Build();

        using var serviceProvider = builder.Services.BuildServiceProvider();
        var progetOptions = serviceProvider.GetRequiredService<ProgetOptions>();

        options = progetOptions.Override
            ? settings : options
            ?? throw new MissingProgetOptionsException(section);

        builder.Services.AddSingleton(options);

        return options;
    }
    
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => !collection?.Any() ?? true;

    public static string ToSnakeCase(this string @string) =>
        string.Concat((@string ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper((@string ?? string.Empty)[i-1]) ? $"_{x}" : x.ToString())).ToLower();

}
