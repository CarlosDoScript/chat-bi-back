namespace Chat.Bi.Infrastructure.Persistence.Repositories;

public class EmpresaRepository (
        ChatBiDbContext context
    ) :
    BaseEntityRepository<Empresa, int>(
        context, context.Set<Empresa>()
    ), IEmpresaRepository
{
}