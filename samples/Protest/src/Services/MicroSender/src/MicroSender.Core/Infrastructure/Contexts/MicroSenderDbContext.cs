using MicroSender.Core.Infrastructure.Configurations;

namespace MicroSender.Core.Infrastructure.Contexts;

internal sealed class MicroSenderDbContext : DbContext
{
    public DbSet<MicroSenderMessage> MicroSenderMessage => Set<MicroSenderMessage>();

    public MicroSenderDbContext(DbContextOptions<MicroSenderDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configuration = new MicroSenderMessageConfiguration();
        modelBuilder.ApplyConfiguration<MicroSenderMessage>(configuration);
    }
}
