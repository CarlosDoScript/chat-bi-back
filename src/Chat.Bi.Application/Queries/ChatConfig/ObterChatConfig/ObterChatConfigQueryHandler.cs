namespace Chat.Bi.Application.Queries.ChatConfig.ObterChatConfig;

public sealed class ObterChatConfigQueryHandler(
    IChatConfigRepository chatConfigRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<ObterChatConfigQuery, Resultado<ChatConfigViewModel>>
{
    public async Task<Resultado<ChatConfigViewModel>> Handle(ObterChatConfigQuery request, CancellationToken cancellationToken)
    {
        var chatConfig = await chatConfigRepository.ObterPorAsync(x => x.Id == request.Id && x.IdEmpresa == usuarioAutenticadoService.ObterIdEmpresa());

        if (chatConfig is null)
            return Resultado<ChatConfigViewModel>.Falhar("Configuração do chat não encontrado.");

        var chatConfigViewModel = new ChatConfigViewModel(
            chatConfig.Id,
            chatConfig.CorPrincipal,
            chatConfig.CorSecundaria,
            chatConfig.SaudacaoInicial,
            chatConfig.Canal,
            chatConfig.ModeloIA,
            chatConfig.Ativo,
            chatConfig.CriadoEm.ToShortDateString()
        );

        return Resultado<ChatConfigViewModel>.Ok(chatConfigViewModel);
    }
}