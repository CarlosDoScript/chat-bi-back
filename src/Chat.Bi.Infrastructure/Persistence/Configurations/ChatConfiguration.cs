namespace Chat.Bi.Infrastructure.Persistence.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Core.Entities.Chat>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Chat> builder)
    {
        builder
            .ToTable(nameof(Core.Entities.Chat))
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
            .HasIndex(x => x.ContextoAnteriorId);

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

        builder.Property(x => x.Origem)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.TokensConsumidos)
            .IsRequired();
        
        builder.Property(x => x.DuracaoRespostaMs)
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

        builder
            .HasOne(x => x.ContextoAnterior)
            .WithMany(x => x.RespostasFilhas)
            .HasForeignKey(x => x.ContextoAnteriorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}