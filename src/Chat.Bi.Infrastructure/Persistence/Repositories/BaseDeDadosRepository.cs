namespace Chat.Bi.Infrastructure.Persistence.Repositories;

public class BaseDeDadosRepository(
    ChatBiDbContext context    
) :
    BaseEntityRepository<BaseDeDados, int>(
        context, context.Set<BaseDeDados>()
    ), IBaseDeDadosRepository
{
}