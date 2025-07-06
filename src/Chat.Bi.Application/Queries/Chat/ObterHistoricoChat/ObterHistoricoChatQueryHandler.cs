using Chat.Bi.Application.ViewModels.Chat;

namespace Chat.Bi.Application.Queries.Chat.ObterHistoricoChat;

public class ObterHistoricoChatQueryHandler(
    IChatRepository chatRepository
) : IRequestHandler<ObterHistoricoChatQuery, Resultado<IEnumerable<ChatHistoricoViewModel>>>
{
    public async Task<Resultado<IEnumerable<ChatHistoricoViewModel>>> Handle(ObterHistoricoChatQuery request, CancellationToken cancellationToken)
    {
        var historico = await chatRepository.ObterHistoricoAsync(request.ChatId);

        if (!historico.Any())
            return Resultado<IEnumerable<ChatHistoricoViewModel>>.Ok(Enumerable.Empty<ChatHistoricoViewModel>());

        var historicoViewModel = historico.Select(x => new ChatHistoricoViewModel(
            x.Id,
            x.TextoPergunta,
            x.TextoResposta,
            x.DataHora,
            x.Origem,
            x.Status,
            x.ContextoAnteriorId
        ));

        return Resultado<IEnumerable<ChatHistoricoViewModel>>.Ok(historicoViewModel);
    }
}