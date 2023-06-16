namespace Proget.Messaging.Email.SendGrid.Extensions;

public static class SendGridConfigurationExtensions
{
    private const string Section = "messaging:mailing:sendgrid";

    public static void AddSendGrid(this IEmailConfigurator configurator,
        Func<ISendGridOptionsBuilder, ISendGridOptionsBuilder>? optionsBuilder = null, string section = Section)
    {
        var builder = configurator.Builder;

        var options = builder.ConfigureOptions<SendGridOptions, SendGridOptionsBuilder, ISendGridOptionsBuilder>(section ?? Section, optionsBuilder);

        if (options.ApiKey is null)
            throw new ApiKeyIsNullException();

        builder.Services.AddSingleton<ISendGridPublisher, SendGridPublisher>();

        configurator.Options.SendGridEnabled = true;
    }
}