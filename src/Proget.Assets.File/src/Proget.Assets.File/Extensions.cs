namespace Proget.Assets.File.Extensions;

public static class Extensions
{
    private const string Section = "assets:file";
    public const string FileDirectory = "Assets";

    public static IAssetsConfigurator AddFile(this IAssetsConfigurator configurator,
        Func<IFileOptionsBuilder, IFileOptionsBuilder>? optionsBuilder = null, bool enabled = true, string section = Section)
    {
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<FileOptions, FileOptionsBuilder, IFileOptionsBuilder>(section ?? Section, optionsBuilder);

        builder.Services.AddSingleton<IFileService, FileService>();

        configurator.Options.FileEnabled = enabled;

        return configurator;
    }
    
    public static IApplicationBuilder UseFileAssetsManager(this IApplicationBuilder app)
    {
        var options = app.ApplicationServices.GetService(typeof(FileOptions)) as FileOptions;

        var directory = options?.Directory ?? FileDirectory;

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        return app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.GetFullPath(directory)),
            RequestPath = PathExtensions.GetPathWithSlashPrefix(directory)
        });
    }
}


