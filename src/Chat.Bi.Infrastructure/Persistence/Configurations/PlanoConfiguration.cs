namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class PlanoConfiguration : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder
            .ToTable(nameof(Plano))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Nome)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(p => p.Ativo)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .Property(x => x.Valor)
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
    }
}
