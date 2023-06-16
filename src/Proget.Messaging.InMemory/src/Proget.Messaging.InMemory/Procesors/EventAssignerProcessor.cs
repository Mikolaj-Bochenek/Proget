namespace Proget.Messaging.InMemory.Processors;

internal sealed class EventAssignerProcessor : BackgroundService
{
    private readonly IChannelAccessor _channelAccessor;
    private readonly IAsyncEventingConsumer _asyncEventingConsumer;

    public EventAssignerProcessor(IChannelAccessor channelAccessor, IAsyncEventingConsumer asyncEventingConsumer)
    {
        _channelAccessor = channelAccessor;
        _asyncEventingConsumer = asyncEventingConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach(var messageEnvelope in _channelAccessor.Reader.ReadAllAsync(stoppingToken))
            _asyncEventingConsumer.Add(messageEnvelope);
    }
}

