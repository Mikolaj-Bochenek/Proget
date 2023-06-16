namespace Proget.Messaging.Email.SendGrid.Builders;

internal class SendGridOptionsBuilder : ISendGridOptionsBuilder
{
    private readonly SendGridOptions _sendGridOptions = new();

    public SendGridOptions Build() => _sendGridOptions;

    public ISendGridOptionsBuilder SetApiKey(string value)
    {
        _sendGridOptions.ApiKey = value;
        return this;
    }
}