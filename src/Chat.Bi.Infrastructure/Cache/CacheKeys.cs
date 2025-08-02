namespace Chat.Bi.Infrastructure.Cache;

public static class CacheKeys
{
    public static string ModeloIa(int empresaId) => $"ModeloIa:Empresa:{empresaId}";
}
