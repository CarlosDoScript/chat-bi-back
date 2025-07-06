namespace Chat.Bi.Core.Repositories;

public interface IChatRepository : IBaseEntityRepository<Entities.Chat,int>
{
    Task<IEnumerable<Entities.Chat>> ObterHistoricoAsync(int id);
}
