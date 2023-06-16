namespace Proget.Messaging.InMemory.Channels;

internal interface IChannelAccessor
{
    ChannelReader<MessageEnvelope> Reader { get; }
    ChannelWriter<MessageEnvelope> Writer { get; }
}
