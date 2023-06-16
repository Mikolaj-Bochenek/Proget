namespace ModularRecipient.Core.Infrastructure.Contexts;

internal sealed class RecipientDbContext : DbContext
{
    public DbSet<RecipientMessage> RecipientMessage => Set<RecipientMessage>();

    public RecipientDbContext(DbContextOptions<RecipientDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("recipient");

        var configuration = new RecipientMessageConfiguration();
        modelBuilder.ApplyConfiguration<RecipientMessage>(configuration);
    }
}
