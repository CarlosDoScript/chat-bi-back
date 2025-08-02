namespace Chat.Bi.Core.Services;

public interface IRagContextoService
{
    Task<(string Contexto,string TipoBaseDeDados)> GerarContextoAsync();
}