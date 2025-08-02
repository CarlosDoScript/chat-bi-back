namespace Chat.Bi.Core.Services;

public interface IQueryGeneratorService
{
    Task<Resultado<(string Sql, string? RespostaUsuario)>> GerarQueryAsync(int empresaId,string perguntaUsuario);
}