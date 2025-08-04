namespace Chat.Bi.Core.Services;

public interface IQueryProcessorService
{
    Task<Resultado<string>> ProcessarPerguntaAsync(int empresaId, string pergunta);
}
