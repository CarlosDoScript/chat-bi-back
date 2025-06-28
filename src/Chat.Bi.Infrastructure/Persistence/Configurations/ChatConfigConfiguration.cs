namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class ChatConfigConfiguration : IEntityTypeConfiguration<ChatConfig>
{
    public void Configure(EntityTypeBuilder<ChatConfig> builder)
    {
        builder
            .ToTable(nameof(ChatConfig))
            .HasKey(x => x.Id);

        builder
            .Property(x => x.CorPrincipal)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.CorSecundaria)
            .HasColumnType("VARCHAR(255)")
            .IsRequired();

        builder
            .Property(x => x.SaudacaoInicial)
            .HasColumnType("VARCHAR(500)")
            .IsRequired();

        builder
            .Property(x => x.Canal)
            .HasColumnType("VARCHAR(255)")
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
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
