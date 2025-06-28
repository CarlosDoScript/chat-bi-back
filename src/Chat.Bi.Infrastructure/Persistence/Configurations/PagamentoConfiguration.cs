namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder
            .ToTable(nameof(Pagamento))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.DataPagamento)
            .HasColumnType("TIMESTAMPTZ")
            .IsRequired();

        builder
            .Property(x => x.Valor)
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.Identificador)
            .HasColumnType("TEXT")
            .IsRequired();

        builder
            .Property(x => x.AlteradoEm)
            .HasColumnType("TIMESTAMPTZ");

        builder
            .HasOne(x => x.Empresa)
            .WithMany()
            .HasForeignKey(x => x.IdEmpresa)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
