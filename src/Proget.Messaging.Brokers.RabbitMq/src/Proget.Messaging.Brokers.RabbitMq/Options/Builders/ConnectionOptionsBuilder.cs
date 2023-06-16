namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public sealed class ConnectionOptionsBuilder : IConnectionOptionsBuilder
{
    private readonly ConnectionOptions _options = new();

    public ConnectionOptions Build()
        => _options;

    public IConnectionOptionsBuilder SetHostName(string? value)
    {
        _options.HostName = value;
        return this;
    }

    public IConnectionOptionsBuilder SetPassword(string? value)
    {
        _options.Password = value;
        return this;
    }

    public IConnectionOptionsBuilder SetPort(int value)
    {
        _options.Port = value;
        return this;
    }

    public IConnectionOptionsBuilder SetUserName(string? value)
    {
        _options.UserName = value;
        return this;
    }

    public IConnectionOptionsBuilder SetVirtualHost(string? value)
    {
        _options.VirtualHost = value;
        return this;
    }
}
