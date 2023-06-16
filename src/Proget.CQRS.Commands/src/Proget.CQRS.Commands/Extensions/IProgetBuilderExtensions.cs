using Microsoft.Extensions.DependencyInjection;
using Proget.CQRS.Commands.Dispatchers;

namespace Proget.CQRS.Commands.Extensions;

public static class IProgetBuilderExtensions
{
    public static IProgetBuilder AddCommands(this IProgetBuilder builder)
    {
        builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        builder.Services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return builder;
    }
}
