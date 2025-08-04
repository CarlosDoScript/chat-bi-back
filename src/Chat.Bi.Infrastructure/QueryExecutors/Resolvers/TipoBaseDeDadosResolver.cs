
using Chat.Bi.Core.Entities;

namespace Chat.Bi.Infrastructure.QueryExecutors.Resolvers;

public class TipoBaseDeDadosResolver(
    IMemoryCache memoryCache,
    ChatBiDbContext context,
    IAppLogger<TipoBaseDeDadosResolver> logger
    ) : ITipoBaseDeDadosResolver
{    
    public async Task<Resultado<string>> ObterTipoBaseDeDadosAsync(int empresaId)
    {
        string cacheKey = CacheKeys.TipoBaseDeDados(empresaId);

        var tipoBase = await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var modelo = await context.BaseDeDados
            .Where(x => x.IdEmpresa == empresaId && x.Ativo)
            .Select(x => x.Tipo)
            .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(modelo))
            {
                logger.LogWarning($"Nenhum Tipo de base de dados configurado para empresa {empresaId}");                
                return string.Empty;
            }

            return modelo;
        });

        if (string.IsNullOrWhiteSpace(tipoBase))
            return Resultado<string>.Falhar("Nenhum Tipo de base de dados foi configurado.");

        return Resultado<string>.Ok(tipoBase);
    }

    public async Task<Resultado<string>> ObterConnectionStringAsync(int empresaId)
    {
        string cacheKey = CacheKeys.ConnectionStringBaseDeDados(empresaId);

        var tipoBase = await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var modelo = await context.BaseDeDados
            .Where(x => x.IdEmpresa == empresaId && x.Ativo)
            .Select(x => x.ConnectionStringCriptografada)
            .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(modelo))
            {
                logger.LogWarning($"Nenhum connection string configurado para empresa {empresaId}");
                return string.Empty;
            }

            return modelo;
        });

        if (string.IsNullOrWhiteSpace(tipoBase))
            return Resultado<string>.Falhar("Nenhuma conexão do banco de dados foi configurado.");

        return Resultado<string>.Ok(tipoBase);
    }
}
