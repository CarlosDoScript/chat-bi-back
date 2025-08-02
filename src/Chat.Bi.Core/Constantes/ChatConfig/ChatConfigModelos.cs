namespace Chat.Bi.Core.Constantes.ChatConfig;

public static class ChatConfigModelos
{
    public const string Ollama = "Ollama";
    public const string OpenIa = "OpenIa";

    public static readonly HashSet<string> Todos = new HashSet<string>
    {
        Ollama,
        OpenIa
    };
}