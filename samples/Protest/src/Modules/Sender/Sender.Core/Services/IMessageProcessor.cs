namespace Sender.Core.Services;

public interface IMessageProcessor
{
    Task ProcessAsync(params IMessage[] messages);
    Task ProcessAsync(IEnumerable<IMessage> messages);
}
