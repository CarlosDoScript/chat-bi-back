namespace Chat.Bi.Infrastructure.IA.Interfaces;

public interface IModelosIaService
{
    Task<string> PerguntarAsync(string prompt, bool stream = false);
}