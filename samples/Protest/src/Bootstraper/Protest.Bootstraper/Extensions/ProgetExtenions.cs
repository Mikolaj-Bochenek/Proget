using Proget.Messaging.CQRS;

namespace Protest.Bootstraper.Extensions;

public static class ProgetExtenions
{
    public static IProgetBuilder AddProgetFramework(this IProgetBuilder builder)
        => builder
            .AddJWT(options => options
                .WithIssuerSigningKey("eiquief5phee9pazo0Faegaez9gohThailiur5woy2befiech1oarai4aiLi6ahVecah3ie9Aiz6Peij")
                .WithExpiryMinutes(60)
                .WithIssuer("protest")
                .WithAudience("protest")
                .WithAudienceValidation(true)
                .WithIssuerValidation(true)
                .WithLifetimeValidation(true)
                .WithAllowAnonymous("/sender/sender/sign-in"))
            .AddCommands()
            .AddEvents()
            .AddQueries()
            .AddMessaging(media => media
                .AddInMemory(options => options
                    .SetLogger()
                    .SetExchange("direct")
                    .SetRoutingKey("direct.default")
                    .SetIgnoreRoutingKeyAttribute(false)));
                
    
    public static IMessageSubscriber UseProgetFramework(this IApplicationBuilder app)
        => app
            .UseAccessTokenValidator()
            .UseMessaging()
            .SubscribeEvent<SenderMessageCreated>()
            .SubscribeEvent<SenderMessageCreated2>();
}
