namespace Chat.Bi.Core.Resolvers;

public interface ITipoBaseDeDadosResolver
{
    Task<Resultado<string>> ObterTipoBaseDeDadosAsync(int empresaId);
    Task<Resultado<string>> ObterConnectionStringAsync(int empresaId);
}