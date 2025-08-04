namespace Chat.Bi.Infrastructure.IA.Resolvers;

public class ModeloIaResolver(
        IMemoryCache memoryCache,
        ChatBiDbContext context,
        IAppLogger<ModeloIaResolver> logger
    ) : IModeloIaResolver
{

    public async Task<string> ObterModeloIaAsync(int empresaId)
    {
        string cacheKey = CacheKeys.TipoBaseDeDados(empresaId);

        return await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var modelo = await context.ChatConfig
            .Where(x => x.IdEmpresa == empresaId && x.Ativo)
            .Select(x => x.ModeloIA)
            .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(modelo))
            {
                logger.LogWarning($"Nenhum modelo IA configurado para empresa {empresaId}");
                return ChatConfigModelos.Ollama;
            }

            return modelo;
        });
    }
}
