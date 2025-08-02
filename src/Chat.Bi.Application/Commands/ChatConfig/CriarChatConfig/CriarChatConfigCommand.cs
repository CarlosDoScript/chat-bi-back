namespace Chat.Bi.Application.Commands.ChatConfig.CriarChatConfig;

public class CriarChatConfigCommand : IRequest<Resultado<int>>
{
    public string CorPrincipal { get; set; }
    public string CorSecundaria { get; set; }
    public string SaudacaoInicial { get; set; }
    public string Canal { get; set; }
    public string ModeloIA { get; set; }
    public bool Ativo { get; set; } = true;
}