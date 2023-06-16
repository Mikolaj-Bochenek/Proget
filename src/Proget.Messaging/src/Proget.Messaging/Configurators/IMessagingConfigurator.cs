namespace Proget.Messaging.Configurators;

public interface IMessagingConfigurator
{
    public IProgetBuilder Builder { get; }
    public MessagingOptions Options { get; }
}
