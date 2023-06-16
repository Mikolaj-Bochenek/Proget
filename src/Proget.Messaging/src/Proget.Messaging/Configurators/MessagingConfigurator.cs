namespace Proget.Messaging.Configurators;

internal sealed class MessagingConfigurator : IMessagingConfigurator
{
    public IProgetBuilder Builder { get; }
    public MessagingOptions Options { get; }

    public MessagingConfigurator(IProgetBuilder builder, MessagingOptions options)
    {
        Builder = builder;
        Options = options;
    }
}
