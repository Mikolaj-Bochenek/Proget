namespace ModularRecipient.Core.Domain.Repositories;

public interface IRecipientRepository
{
    Task<IEnumerable<RecipientMessage>> GetAll();
    Task CreateAsync(RecipientMessage recipientMessage);
}
