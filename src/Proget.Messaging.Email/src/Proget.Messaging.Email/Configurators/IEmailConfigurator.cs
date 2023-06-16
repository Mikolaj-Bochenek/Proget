namespace Proget.Messaging.Email.Configurators;

public interface IEmailConfigurator
{
    public EmailOptions Options { get; }
    public IProgetBuilder Builder { get; }
}
