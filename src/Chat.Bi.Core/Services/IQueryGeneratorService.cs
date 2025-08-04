namespace Chat.Bi.Core.Services;

public interface IQueryGeneratorService
{
    Task<Resultado<string>> GerarQueryAsync(int empresaId,string perguntaUsuario);
}