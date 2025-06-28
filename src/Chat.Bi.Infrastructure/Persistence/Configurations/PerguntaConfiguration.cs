namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class PerguntaConfiguration : IEntityTypeConfiguration<Pergunta>
{
    public void Configure(EntityTypeBuilder<Pergunta> builder)
    {
        builder
            .ToTable(nameof(Pergunta))
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => x.DataHora);
        
        builder
            .HasIndex(x => x.IdUsuario);

        builder
            .HasIndex(x => new { x.IdUsuario, x.DataHora});
        
        builder
            .HasIndex(x => x.IdBaseDeDados);
        
        builder
            .HasIndex(x => x.IdIntegracaoApi);

        builder
            .Property(x => x.TextoPergunta)
            .HasColumnType("TEXT")
            .IsRequired();
        
        builder
            .Property(x => x.TextoResposta)
            .HasColumnType("TEXT")
            .IsRequired();
        
        builder            
            .Property(x => x.DataHora)            
            .HasColumnType("TIMESTAMPTZ")            
            .IsRequired();

        builder
            .HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey(x => x.IdUsuario)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder
            .HasOne(x => x.BaseDeDados)
            .WithMany()
            .HasForeignKey(x => x.IdBaseDeDados)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.IntegracaoApi)
            .WithMany()
            .HasForeignKey(x => x.IdIntegracaoApi)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
