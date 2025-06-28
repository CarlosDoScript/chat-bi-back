namespace Chat.Bi.Core.Repositories;

public interface IUsuarioRepository : IBaseEntityRepository<Entities.Usuario, int>
{
    Task<bool> ExisteAsync(string documento, string email);
}