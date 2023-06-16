namespace Proget.ModularMonolith;

public sealed record AppContext(List<Assembly> LoadedAssemblies, HashSet<IModule> LoadedModules);
