
using Microsoft.EntityFrameworkCore.Migrations;

namespace Chat.Bi.Infrastructure.Persistence;

public partial class ChatBiDbContext : DbContext
{
    public ChatBiDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public partial class ChatBiDbContext
{
    public DbSet<BaseDeDados> BaseDeDados => Set<BaseDeDados>();
    public DbSet<ChatConfig> ChatConfig => Set<ChatConfig>();
    public DbSet<ConsumoToken> ConsumoToken => Set<ConsumoToken>();
    public DbSet<Empresa> Empresa => Set<Empresa>();
    public DbSet<IntegracaoApi> IntegracaoApi => Set<IntegracaoApi>();
    public DbSet<Pagamento> Pagamento => Set<Pagamento>();
    public DbSet<Core.Entities.Chat> Pergunta => Set<Core.Entities.Chat>();
    public DbSet<Plano> Plano => Set<Plano>();
    public DbSet<Usuario> Usuario => Set<Usuario>();
}