namespace Chat.Bi.Infrastructure.Persistence.Repositories;

public class ChatConfigRepository(
        ChatBiDbContext context
    ) :
    BaseEntityRepository<ChatConfig, int>(
        context, context.Set<ChatConfig>()
    ), IChatConfigRepository
{
}
