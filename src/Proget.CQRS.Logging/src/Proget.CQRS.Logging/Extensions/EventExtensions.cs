using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Proget;
using Proget.CQRS.Commands;
using Scrutor;
using Proget.CQRS.Events;
using Proget.CQRS.Logging.Decorators;

namespace Proget.CQRS.Logging.Extensions;

public static class EventExtensions
{
    [Description("IEventHandler has to be registered first, before this method is called")]
    public static IProgetBuilder AddEventHandlersLogging<TMapper>(this IProgetBuilder builder)
        where TMapper : class, ILoggingMapper
    {
        builder.Services.AddSingleton<ILoggingMapper, TMapper>();
        builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(EventHandlerLoggingDecorator<>));

        return builder;
    }

    [Description("IEventHandler has to be registered first, before this method is called")]
    public static IProgetBuilder AddEventHandlersLogging(this IProgetBuilder builder)
    {
        builder.Services.Scan(source => source
            .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(@class => @class.AssignableTo(typeof(ILoggingMapper)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        builder.Services.TryDecorate(typeof(IEventHandler<>), typeof(EventHandlerLoggingDecorator<>));

        return builder;
    }
}
