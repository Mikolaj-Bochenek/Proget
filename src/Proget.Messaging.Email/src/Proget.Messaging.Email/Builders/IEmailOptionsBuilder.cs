namespace Proget.Messaging.Email.Builders;

public interface IEmailOptionsBuilder : IProgetOptionsBuilder<EmailOptions>
{
    IEmailOptionsBuilder SetSenderAddress(string value);
    IEmailOptionsBuilder SetSenderName(string value);
}
