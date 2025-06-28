namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class BaseDeDadosConfiguration : IEntityTypeConfiguration<BaseDeDados>
{
    public void Configure(EntityTypeBuilder<BaseDeDados> builder)
    {
        builder
            .ToTable(nameof(BaseDeDados))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Nome)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.Ativo)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .Property (x => x.ConnectionStringCriptografada)
            .HasColumnType("TEXT")
            .IsRequired();

        builder
            .Property(x => x.SomenteLeitura)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .Property(p => p.AlteradoEm)
            .HasColumnType("TIMESTAMPTZ");

        builder
            .HasOne(x => x.Empresa)
            .WithMany()
            .HasForeignKey(x => x.IdEmpresa)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
