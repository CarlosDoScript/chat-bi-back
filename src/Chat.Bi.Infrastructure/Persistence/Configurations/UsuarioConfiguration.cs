namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .ToTable(nameof(Usuario))
            .HasKey(x => x.Id);

        builder
             .Property(x => x.Nome)
             .HasColumnType("VARCHAR(255)")
             .IsRequired();
        
        builder
             .Property(x => x.UltimoLogin)
             .HasColumnType("TIMESTAMPTZ");
   
        builder
             .Property(x => x.RefreshToken)
             .HasColumnType("TEXT");
     
        builder
             .Property(x => x.RefreshTokenExpiracao)
             .HasColumnType("TIMESTAMPTZ");

        builder
             .Property(x => x.RecuperacaoAcessoCodigo)
             .HasColumnType("TEXT");
        
        builder
             .Property(x => x.RecuperacaoAcessoValidade)
             .HasColumnType("TIMESTAMPTZ");

        builder
             .Property(x => x.Email)
             .HasColumnType("VARCHAR(255)")
             .IsRequired();

        builder
             .Property(x => x.SenhaHash)
             .HasColumnType("TEXT")
             .IsRequired();

        builder
            .Property(x => x.Documento)
            .HasColumnType("VARCHAR(20)")
            .IsRequired();

        builder
            .Property(p => p.DataNascimento)
            .HasColumnType("TIMESTAMPTZ");

        builder
            .Property(p => p.AlteradoEm)
            .HasColumnType("TIMESTAMPTZ");

        builder
            .Property(p => p.Ativo)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .Property(p => p.Admin)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .Property(p => p.Master)
            .HasColumnType("BOOLEAN")
            .IsRequired();

        builder
            .HasOne(x => x.Empresa)
            .WithMany()
            .HasForeignKey(x => x.IdEmpresa)
            .IsRequired();
    }
}