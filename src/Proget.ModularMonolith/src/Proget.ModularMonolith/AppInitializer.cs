namespace Proget.ModularMonolith;

public static class AppInitializer
{
    public static AppContext Initialize(WebApplicationBuilder builder, string modulePrefix)
    {
        var disabledModules = new HashSet<string>();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        foreach (var file in files)
        {
            if (!file.Contains(modulePrefix))
            {
                continue;
            }

            var moduleName = file.Split($"{modulePrefix}.").Last().Split(".").First().ToLowerInvariant();
            var enabled = builder.Configuration.GetValue<bool>($"{moduleName}:module:enabled");
            
            if (enabled)
            {
                assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file)));
            }
            else
            {
                disabledModules.Add(moduleName);
            }
        }

        builder.Services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            var appParts = disabledModules.SelectMany(x => manager.ApplicationParts
                .Where(part => part.Name.Contains(x, StringComparison.InvariantCultureIgnoreCase))).ToList();

            foreach (var part in appParts)
            {
                manager.ApplicationParts.Remove(part);
            }
                    
            manager.FeatureProviders.Add(new CustomControllerFeatureProvider());
        });

        var modules = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IModule).IsAssignableFrom(x) && x.IsClass)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();
        
        return new (assemblies.ToList(), modules.ToHashSet());
    }
}