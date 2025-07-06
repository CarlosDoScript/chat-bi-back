using Chat.Bi.Application.ViewModels.Chat;

namespace Chat.Bi.Application.Queries.Chat.ObterHistoricoChat;

public class ObterHistoricoChatQuery : IRequest<Resultado<IEnumerable<ChatHistoricoViewModel>>>
{
    public int ChatId { get; set; }
}
