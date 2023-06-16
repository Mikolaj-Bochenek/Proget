namespace Proget.Messaging.Email.Smtp.Builders;

internal class SmtpOptionsBuilder : ISmtpOptionsBuilder
{
    private readonly SmtpOptions _smtpOptions = new();

    public SmtpOptions Build() => _smtpOptions;

    public ISmtpOptionsBuilder SetSslEnabled(bool value = true)
    {
        _smtpOptions.SslEnabled = value;
        return this;
    }

    public ISmtpOptionsBuilder SetHost(string host)
    {
        _smtpOptions.Host = host;
        return this;
    }

    public ISmtpOptionsBuilder SetPort(int value)
    {
        _smtpOptions.Port = value;
        return this;
    }

    public ISmtpOptionsBuilder SetPassword(string value)
    {
        _smtpOptions.Password = value;
        return this;
    }
}
