namespace MicroSender.Core.Infrastructure.Configurations;

internal sealed class MicroSenderMessageConfiguration : IEntityTypeConfiguration<MicroSenderMessage>
{
    public void Configure(EntityTypeBuilder<MicroSenderMessage> builder)
    {
        builder.ToTable(name: "MicroSenderMessage");
        builder.HasKey(x => x.Id);
    }
}