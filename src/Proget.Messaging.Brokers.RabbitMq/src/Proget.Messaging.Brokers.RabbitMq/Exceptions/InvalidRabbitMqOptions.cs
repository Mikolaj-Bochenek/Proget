namespace Proget.Messaging.Brokers.RabbitMq.Exceptions;

internal sealed class InvalidRabbitMqOptions : Exception
{
    private const string? ErrorMessage = "The Proget.Messaging.Brokers.RabbitMQ configuration is missing."
        + " To work with this package properly add the defualt 'messaging:brokers:rabbitmq' section in appsettings.json"
        + " or use OptionsBuilder via lambda expression in Program.cs";

    public InvalidRabbitMqOptions() : base(ErrorMessage)
    {
    }
}