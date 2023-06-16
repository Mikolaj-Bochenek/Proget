namespace Proget.Messaging.SMSApi.Builders;

internal sealed class SmsApiOptionsBuilder : ISmsApiOptionsBuilder
{
    private readonly SmsApiOptions _smsApiOptions = new();

    public SmsApiOptions Build() => _smsApiOptions;

    public ISmsApiOptionsBuilder SetApiKey(string value)
    {
        _smsApiOptions.ApiKey = value;
        return this;
    }

    public ISmsApiOptionsBuilder SetSenderName(string value)
    {
        _smsApiOptions.SenderName = value;
        return this;
    }
}
