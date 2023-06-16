namespace Proget.CQRS.Queries.Extensions;

public static class IProgetBuilderExtensions
{
    public static IProgetBuilder AddQueries(this IProgetBuilder builder)
    {
        builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        builder.Services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        
        return builder;
    }
}
