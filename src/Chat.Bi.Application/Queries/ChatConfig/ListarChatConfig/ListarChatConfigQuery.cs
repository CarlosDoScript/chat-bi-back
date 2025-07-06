using Chat.Bi.Application.ViewModels.ChatConfig;

namespace Chat.Bi.Application.Queries.ChatConfig.ListarChatConfig;

public class ListarChatConfigQuery : ConsultaPaginada, IRequest<Resultado<PaginacaoViewModel<ChatConfigViewModel>>>
{
}