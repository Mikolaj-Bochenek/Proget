namespace Proget.Messaging.Email.SendGrid.Builders;

public interface ISendGridOptionsBuilder : IProgetOptionsBuilder<SendGridOptions>
{
    ISendGridOptionsBuilder SetApiKey(string value);
}