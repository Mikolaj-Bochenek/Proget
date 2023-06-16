namespace MicroSender.Core.Infrastructure.Repositories;

internal sealed class MicroSenderRepository : IMicroSenderRepository
{
    private readonly MicroSenderDbContext _context;
    private readonly DbSet<MicroSenderMessage> _MicroSenderMessage;

    public MicroSenderRepository(MicroSenderDbContext context)
    {
        _context = context;
        _MicroSenderMessage = context.MicroSenderMessage;
    }

    public async Task CreateAsync(MicroSenderMessage MicroSenderMessage)
    {
        await _MicroSenderMessage.AddAsync(MicroSenderMessage);
        await _context.SaveChangesAsync();
    }
}
