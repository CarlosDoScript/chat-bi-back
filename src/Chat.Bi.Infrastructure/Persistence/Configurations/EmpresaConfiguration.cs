namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder
            .ToTable(nameof(Empresa))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Nome)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.Documento)
            .HasColumnType("VARCHAR(20)")
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasColumnType("VARCHAR(100)")
            .IsRequired();

        builder
            .Property(p => p.AlteradoEm)
            .HasColumnType("TIMESTAMPTZ");

        builder
            .Property(p => p.Ativo)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .HasOne(x => x.Plano)
            .WithMany()
            .HasForeignKey(x => x.IdPlano)
            .IsRequired();
    }
}
