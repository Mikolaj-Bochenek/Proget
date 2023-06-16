namespace Proget.Messaging.Email.Smtp.Builders;

public interface ISmtpOptionsBuilder : IProgetOptionsBuilder<SmtpOptions>
{
    ISmtpOptionsBuilder SetSslEnabled(bool value = true);
    ISmtpOptionsBuilder SetHost(string host);
    ISmtpOptionsBuilder SetPort(int value);
    ISmtpOptionsBuilder SetPassword(string value);
}
