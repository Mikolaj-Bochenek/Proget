namespace Proget.Messaging.Email.Smtp.Extensions;

public static class SmtpConfigurationExtensions
{
    private const string Section = "messaging:mailing:smtp";
    public static void AddSmtp(this IEmailConfigurator configurator,
        Func<ISmtpOptionsBuilder, ISmtpOptionsBuilder>? optionsBuilder = null, string section = Section)
    {
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<SmtpOptions, SmtpOptionsBuilder, ISmtpOptionsBuilder>(section ?? Section, optionsBuilder);

        if (options.Host is null)
                throw new SmtpHostAddressIsNullException();

        if (options.Password is null)
            throw new SmtpPasswordIsNullException();

        builder.Services.AddSingleton<ISmtpPublisher, SmtpPublisher>();

        configurator.Options.SmtpEnabled = true;
    }
}
