namespace Chat.Bi.Infrastructure.Persistence.Repositories;

public class UsuarioRepository(
        ChatBiDbContext context
    ) :
    BaseEntityRepository<Usuario, int>(
        context, context.Set<Usuario>()
    ), IUsuarioRepository
{
    public async Task<bool> ExisteAsync(string documento, string email)
    {
        return await context.Usuario
            .AsNoTracking()
            .AnyAsync(x => x.Documento == documento || x.Email == email);
    }
}