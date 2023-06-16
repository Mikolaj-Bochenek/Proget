namespace Sender.Core.Infrastructure.Configurations;

internal sealed class SenderMessageConfiguration : IEntityTypeConfiguration<SenderMessage>
{
    public void Configure(EntityTypeBuilder<SenderMessage> builder)
    {
        builder.ToTable(name: "SenderMessage");
        builder.HasKey(x => x.Id);
    }
}