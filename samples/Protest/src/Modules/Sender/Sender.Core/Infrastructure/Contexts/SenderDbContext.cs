namespace Sender.Core.Infrastructure.Contexts;

internal sealed class SenderDbContext : DbContext
{
    public DbSet<SenderMessage> SenderMessage => Set<SenderMessage>();

    public SenderDbContext(DbContextOptions<SenderDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("sender");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
