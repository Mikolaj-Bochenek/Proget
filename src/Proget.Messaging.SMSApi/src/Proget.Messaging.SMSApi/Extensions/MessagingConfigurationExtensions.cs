namespace Proget.Messaging.SMSApi.Extensions;

public static class MessagingConfigurationExtensions
{
    private const string Section = "messaging:smsapi";

    public static IMessagingConfigurator AddSmsApi(this IMessagingConfigurator configurator,
        Func<ISmsApiOptionsBuilder, ISmsApiOptionsBuilder>? optionsBuilder = null, string section = Section)
    {
        if (string.IsNullOrWhiteSpace(section))
            section = Section;

        var builder = configurator.Builder;

        builder.ConfigureOptions<SmsApiOptions, SmsApiOptionsBuilder, ISmsApiOptionsBuilder>(section ?? Section, optionsBuilder);

        builder.Services.AddSingleton<ISmsPublisher, SmsPublisher>();

        configurator.Options.SmsEnabled = true;

        return configurator;
    }
}
