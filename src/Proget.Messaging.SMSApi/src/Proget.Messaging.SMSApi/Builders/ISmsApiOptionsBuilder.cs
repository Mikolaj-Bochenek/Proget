namespace Proget.Messaging.SMSApi.Builders;

public interface ISmsApiOptionsBuilder : IProgetOptionsBuilder<SmsApiOptions>
{
    ISmsApiOptionsBuilder SetApiKey(string value);
    ISmsApiOptionsBuilder SetSenderName(string value);
}
