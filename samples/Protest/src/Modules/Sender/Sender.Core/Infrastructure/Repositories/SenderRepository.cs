namespace Sender.Core.Infrastructure.Repositories;

internal sealed class SenderRepository : ISenderRepository
{
    private readonly SenderDbContext _context;
    private readonly DbSet<SenderMessage> _senderMessage;

    public SenderRepository(SenderDbContext context)
    {
        _context = context;
        _senderMessage= context.SenderMessage;
    }

    public async Task CreateAsync(SenderMessage senderMessage)
    {
        await _senderMessage.AddAsync(senderMessage);
        await _context.SaveChangesAsync();
    }
}
