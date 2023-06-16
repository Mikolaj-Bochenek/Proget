namespace Proget.Messaging.Email.Extensions;

public static class EmailConfigurationExtensions
{
    private const string Section = "messaging:mailing";
    public static IMessagingConfigurator AddMailing(this IMessagingConfigurator configurator,
        Func<IEmailOptionsBuilder, IEmailOptionsBuilder>? optionsBuilder = null,
        Action<IEmailConfigurator>? configure = null, bool enabled = true, string section = Section)
    {
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<EmailOptions, EmailOptionsBuilder, IEmailOptionsBuilder>(section ?? Section, optionsBuilder);

        if (options.SenderAddress is null)
            throw new SenderAddressIsNullException();
        
        var emailConfigurator = new EmailConfigurator(options, builder);

        if (configure is not null)
            configure(emailConfigurator);

        builder.Services.AddSingleton<IEmailPublisher, EmailPublisher>();

        configurator.Options.MailingEnabled = enabled;

        return configurator;
    }
}
