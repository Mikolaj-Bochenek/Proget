namespace MicroSender.Core.Domain.Repositories;

public interface IMicroSenderRepository
{
    Task CreateAsync(MicroSenderMessage MicroSenderMessage);
}
