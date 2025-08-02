namespace Chat.Bi.Infrastructure.Configuration;

public class ModelosIaSettings
{
    public OllamaSettings Ollama { get; set; }
}

public class OllamaSettings
{
    public string Endpoint { get; set; }
    public string Model { get; set; }
}