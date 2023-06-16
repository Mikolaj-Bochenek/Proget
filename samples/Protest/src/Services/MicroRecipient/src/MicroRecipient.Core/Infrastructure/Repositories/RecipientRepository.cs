namespace MicroRecipient.Core.Infrastructure.Repositories;

internal sealed class RecipientRepository : IRecipientRepository
{
    private readonly RecipientDbContext _context;
    private readonly DbSet<RecipientMessage> _recipientMessage;

    public RecipientRepository(RecipientDbContext context)
    {
        _context = context;
        _recipientMessage= context.RecipientMessage;
    }

    public async Task<IEnumerable<RecipientMessage>> GetAll()
        => await _recipientMessage.ToListAsync();

    public async Task CreateAsync(RecipientMessage recipientMessage)
    {
        await _recipientMessage.AddAsync(recipientMessage);
        await _context.SaveChangesAsync();
    }

}
