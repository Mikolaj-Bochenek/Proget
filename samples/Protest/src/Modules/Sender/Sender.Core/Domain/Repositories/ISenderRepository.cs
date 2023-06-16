namespace Sender.Core.Domain.Repositories;

public interface ISenderRepository
{
    Task CreateAsync(SenderMessage senderMessage);
}
