using Chat.Bi.Application.ViewModels.ChatConfig;

namespace Chat.Bi.Application.Queries.ChatConfig.ObterChatConfig;

public class ObterChatConfigQuery : IRequest<Resultado<ChatConfigViewModel>>
{
    public int Id { get; set; }
}
