namespace Proget.Messaging.Brokers.RabbitMq.Connections;

internal sealed class ChannelFactory : IChannelFactory
{
    private readonly IConnection _connection;
    private readonly ChannelAccessor _channelAccessor;

    public ChannelFactory(IConnection connection, ChannelAccessor channelAccessor)
        => (_connection, _channelAccessor) = (connection, channelAccessor);

    public IModel Create()
        => _channelAccessor.Channel
        ?? (_channelAccessor.Channel = _connection.CreateModel());
}
