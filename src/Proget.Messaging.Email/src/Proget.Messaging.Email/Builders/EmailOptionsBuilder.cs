namespace Proget.Messaging.Email.Builders;

public sealed class EmailOptionsBuilder : IEmailOptionsBuilder
{
    private readonly EmailOptions _options = new();

    public EmailOptions Build()
        => _options;

    public IEmailOptionsBuilder SetSenderAddress(string value)
    {
        _options.SenderAddress = value;
        return this;
    }

    public IEmailOptionsBuilder SetSenderName(string value)
    {
        _options.SenderName = value;
        return this;
    }
}
