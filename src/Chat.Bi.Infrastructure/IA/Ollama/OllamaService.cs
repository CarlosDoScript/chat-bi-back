using Chat.Bi.Core.Constantes.ChatConfig;

namespace Chat.Bi.Infrastructure.IA.Ollama;

public class OllamaService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IAppLogger<OllamaService> logger
    ) : IModelosIaService
{
    HttpClient _httpClient
        => httpClientFactory.CreateClient(ApisConfiguration.Ollama);
    
    readonly OllamaSettings _ollamaSettings = configuration.GetNested<ModelosIaSettings>().Ollama;

    public string Nome 
        => ChatConfigModelos.Ollama;

    public async Task<string> PerguntarAsync(string prompt, bool stream = false)
    {
        var request = new
        {
            model = _ollamaSettings.Model,
            prompt,
            stream
        };
        
        var response = await _httpClient.PostAsJsonAsync("api/generate",request);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
}