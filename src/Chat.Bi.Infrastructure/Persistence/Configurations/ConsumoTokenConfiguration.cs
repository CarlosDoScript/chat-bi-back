namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class ConsumoTokenConfiguration : IEntityTypeConfiguration<ConsumoToken>
{
    public void Configure(EntityTypeBuilder<ConsumoToken> builder)
    {
        builder
            .ToTable(nameof(ConsumoToken))
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.Empresa)
            .WithMany()
            .HasForeignKey(x => x.IdEmpresa)
            .IsRequired();
    }
}
