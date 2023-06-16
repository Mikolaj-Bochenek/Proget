namespace Proget.ORM.EntityFramework.Extensions;

public static class EntityFrameworkExtensions
{
    public static IServiceCollection AddEntityFrameworkInitializer<TContext>(this IServiceCollection services, bool withSeeder = true)
        where TContext : DbContext
        => services.AddHostedService(h => new EntityFrameworkInitializer<TContext>(
            h.GetRequiredService<IServiceProvider>(),
            withSeeder));

    public static IServiceCollection AddEntityFrameworkGlobalInitializer(this IServiceCollection services)
        => services.AddHostedService<EntityFrameworkGlobalInitializer>();

    public static IProgetBuilder AddEntityFrameworkDbContext<TContext>(this IProgetBuilder builder,
        Func<IEFOptionsBuilder<TContext>, IEFOptionsBuilder<TContext>> optionsBuilder) where TContext : DbContext
    {
        var funcOptions = optionsBuilder?.Invoke(new EFOptionsBuilder<TContext>()).Build();

        if (funcOptions!.ConnectionString.IsNullOrEmpty())
            throw new MissingConnectionStringException();

        if (funcOptions.GenericRepositoryType is not null)
            builder.Services.AddScoped(typeof(IGenericRepository<>), funcOptions.GenericRepositoryType);
        else
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        if (funcOptions.RegisterRepositories)
            RegisterRepositories();

        switch (funcOptions.DatabaseProvider)
        {
            case DatabaseProviders.SqlServer:
                UseSqlServer();
                break;
            default:
                UseSqlServer();
                break;
        }

        return builder;

        void UseSqlServer()
        {
            builder.Services.AddDbContext<TContext>(ctx => ctx.UseSqlServer(funcOptions.ConnectionString));

            if (funcOptions.SeederType is not null)
                builder.Services.AddTransient(typeof(IDbContextSeeder<TContext>), funcOptions.SeederType);

            if (funcOptions.Init)
                builder.Services.AddEntityFrameworkInitializer<TContext>(funcOptions.Seed);
        }

        void RegisterRepositories()
        {
            var assembly = typeof(TContext).Assembly;

            var repositoryInterfaces = GetIGenericRepositoryChilds(assembly);
            var repositoryWithPagingInterfaces = GetIGenericRepositoryWithPagingChilds(assembly);

            repositoryInterfaces?.ToList().ForEach(intrf =>
            {
                builder.Services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(intrf))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            });

            repositoryWithPagingInterfaces?.ToList().ForEach(intrf =>
            {
                builder.Services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(intrf))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            });
        }

        IEnumerable<Type>? GetIGenericRepositoryChilds(Assembly assembly)
            => assembly.GetTypes().Where(x => x.IsInterface && x.GetInterfaces().Any(x =>
                    x.Name == typeof(IGenericRepository<>).Name &&
                    x.Namespace is not null && x.Namespace == typeof(IGenericRepository<>).Namespace));

        IEnumerable<Type>? GetIGenericRepositoryWithPagingChilds(Assembly assembly)
            => assembly.GetTypes().Where(x => x.IsInterface && x.GetInterfaces().Any(x =>
                    x.Name == typeof(IGenericRepositoryWithPaging<>).Name &&
                    x.Namespace is not null && x.Namespace == typeof(IGenericRepositoryWithPaging<>).Namespace));
    }
}
