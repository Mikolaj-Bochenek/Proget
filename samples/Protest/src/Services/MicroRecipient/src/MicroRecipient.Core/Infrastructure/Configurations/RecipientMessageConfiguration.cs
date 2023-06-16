namespace MicroRecipient.Core.Infrastructure.Configurations;

internal sealed class RecipientMessageConfiguration : IEntityTypeConfiguration<RecipientMessage>
{
    public void Configure(EntityTypeBuilder<RecipientMessage> builder)
    {
        builder.ToTable(name: "RecipientMessage");
        builder.HasKey(x => x.Id);
    }
}