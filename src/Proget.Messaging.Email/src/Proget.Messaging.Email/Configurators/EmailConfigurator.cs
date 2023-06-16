namespace Proget.Messaging.Email.Configurators;

public record EmailConfigurator(EmailOptions Options, IProgetBuilder Builder): IEmailConfigurator { }