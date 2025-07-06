
namespace Chat.Bi.Infrastructure.Persistence.Repositories;

public class ChatRepository(
        ChatBiDbContext context
    ) :
    BaseEntityRepository<Core.Entities.Chat, int>(
        context, context.Set<Core.Entities.Chat>()
    ), IChatRepository
{
    public async Task<IEnumerable<Core.Entities.Chat>> ObterHistoricoAsync(int id)
    {
        var historico = new List<Core.Entities.Chat>();

        var chat = await context.Chat
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        while (chat is not null)
        {
            historico.Add(chat);

            if (chat.ContextoAnteriorId is null)
                break;

            chat = await context.Chat
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == chat.ContextoAnteriorId);
        }

        return historico
            .OrderBy(x => x.DataHora);
    }
}
