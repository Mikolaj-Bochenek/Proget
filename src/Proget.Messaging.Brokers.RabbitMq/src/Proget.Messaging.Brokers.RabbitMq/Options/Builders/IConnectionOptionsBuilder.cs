namespace Proget.Messaging.Brokers.RabbitMq.Options.Builders;

public interface IConnectionOptionsBuilder
{
    ConnectionOptions Build();
    IConnectionOptionsBuilder SetHostName(string? value);
    IConnectionOptionsBuilder SetVirtualHost(string? value);
    IConnectionOptionsBuilder SetPort(int value);
    IConnectionOptionsBuilder SetUserName(string? value);
    IConnectionOptionsBuilder SetPassword(string? value);
}