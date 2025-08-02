namespace Chat.Bi.Core.Services;

public interface IModelosIaService
{
    string Nome { get; }
    Task<string> PerguntarAsync(string prompt, bool stream = false);
}