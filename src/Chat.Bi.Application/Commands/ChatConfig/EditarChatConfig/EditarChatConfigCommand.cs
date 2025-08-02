namespace Chat.Bi.Application.Commands.ChatConfig.EditarChatConfig;

public class EditarChatConfigCommand : IRequest<Resultado<ChatConfigViewModel>>
{
    internal int Id { get; set; }
    public string CorPrincipal { get; set; }
    public string CorSecundaria { get; set; }
    public string SaudacaoInicial { get; set; }
    public string Canal { get; set; }
    public string  ModeloIA { get; set; }
    public bool Ativo { get; set; }

    public void SetId(int id)
      => Id = id;
}