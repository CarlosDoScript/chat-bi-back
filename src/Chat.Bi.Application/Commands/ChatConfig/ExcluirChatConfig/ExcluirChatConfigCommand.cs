namespace Chat.Bi.Application.Commands.ChatConfig.ExcluirChatConfig;

public class ExcluirChatConfigCommand : IRequest<Resultado>
{
    public int Id { get; set; }
}
