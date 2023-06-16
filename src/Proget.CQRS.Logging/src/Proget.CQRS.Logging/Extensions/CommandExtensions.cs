using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Proget;
using Proget.CQRS.Commands;
using Proget.CQRS.Logging.Decorators;

namespace Proget.CQRS.Logging.Extensions;

public static class CommandExtensions
{
    [Description("ICommandHandler has to be registered first, before this method is called")]
    public static IProgetBuilder AddCommandHandlersLogging<TMapper>(this IProgetBuilder builder)
        where TMapper : class, ILoggingMapper
    {
        builder.Services.AddSingleton<ILoggingMapper, TMapper>();
        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(CommandHandlerLoggingDecorator<>));

        return builder;
    }

    [Description("ICommandHandler has to be registered first, before this method is called")]
    public static IProgetBuilder AddCommandHandlersLogging(this IProgetBuilder builder)
    {
        builder.Services.Scan(source => source
            .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(@class => @class.AssignableTo(typeof(ILoggingMapper)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(CommandHandlerLoggingDecorator<>));

        return builder;
    }
}
