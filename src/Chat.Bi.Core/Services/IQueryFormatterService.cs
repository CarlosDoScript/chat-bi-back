namespace Chat.Bi.Core.Services;

public interface IQueryFormatterService
{
    Task<Resultado<string>> FormatarResultadoAsync(DataTable data, string perguntaUsuario, int empresaId);
}