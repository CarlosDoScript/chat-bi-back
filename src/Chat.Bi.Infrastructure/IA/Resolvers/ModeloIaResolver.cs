namespace Chat.Bi.Infrastructure.IA.Resolvers;

public class ModeloIaResolver(
        IMemoryCache memoryCache,
        ChatBiDbContext context,
        IAppLogger<ModeloIaResolver> logger
    ) : IModeloIaResolver
{

    public async Task<string> ObterModeloIaAsync(int empresaid)
    {
        string cacheKey = CacheKeys.ModeloIa(empresaid);

        return await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

            var modelo = await context.ChatConfig
            .Where(x => x.IdEmpresa == empresaid && x.Ativo)
            .Select(x => x.ModeloIA)
            .FirstOrDefaultAsync();

            if (string.IsNullOrWhiteSpace(modelo))
            {
                logger.LogWarning($"Nenhum modelo IA configurado para empresa {empresaid}");
                return ChatConfigModelos.Ollama;
            }

            return modelo;
        });
    }
}
