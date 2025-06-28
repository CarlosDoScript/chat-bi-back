namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class IntegracaoApiConfiguration : IEntityTypeConfiguration<IntegracaoApi>
{
    public void Configure(EntityTypeBuilder<IntegracaoApi> builder)
    {
        builder
            .ToTable(nameof(IntegracaoApi))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Nome)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.Url)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.MetodoHttp)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.Autenticacao)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.SchemaResposta)
            .HasColumnType("TEXT")
            .IsRequired();

        builder
            .Property(x => x.CorpoRequisicao)
            .HasColumnType("TEXT")
            .IsRequired();

        builder
            .Property(x => x.HeadersJson)
            .HasColumnType("TEXT")
            .IsRequired();

        builder
            .Property(x => x.Ativo)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .Property(p => p.AlteradoEm)
            .HasColumnType("TIMESTAMPTZ");

        builder
            .HasOne(x => x.Empresa)
            .WithMany()
            .HasForeignKey(x => x.IdEmpresa)
            .IsRequired();
    }
}
