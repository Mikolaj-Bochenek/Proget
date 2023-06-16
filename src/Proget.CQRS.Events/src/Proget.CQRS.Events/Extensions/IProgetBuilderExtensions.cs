using Microsoft.Extensions.DependencyInjection;
using Proget;
using Proget.Attributes;
using Proget.CQRS.Events.Dispatchers;

namespace Proget.CQRS.Events.Extensions;

public static class IProgetBuilderExtensions
{
    public static IProgetBuilder AddEvents(this IProgetBuilder builder)
    {
        builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();

        builder.Services.Scan(source => source
            .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddClasses(@class => @class.AssignableTo(typeof(IEventHandler<>))
                .WithoutAttribute(typeof(Decorator)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return builder;
    }
}
