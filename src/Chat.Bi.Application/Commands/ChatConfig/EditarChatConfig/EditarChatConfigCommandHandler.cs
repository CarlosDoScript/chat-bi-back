namespace Chat.Bi.Application.Commands.ChatConfig.EditarChatConfig;

public sealed class EditarChatConfigCommandHandler(
    IChatConfigRepository chatConfigRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<EditarChatConfigCommand, Resultado<ChatConfigViewModel>>
{
    public async Task<Resultado<ChatConfigViewModel>> Handle(EditarChatConfigCommand request, CancellationToken cancellationToken)
    {
        var chatConfig = await chatConfigRepository.ObterPorAsync(x => x.Id == request.Id && x.IdEmpresa == usuarioAutenticadoService.ObterIdEmpresa());

        if (chatConfig is null)
            return Resultado<ChatConfigViewModel>.Falhar("Configuração do chat não encontrado.");

        chatConfig.Alterar(
            request.CorPrincipal,
            request.CorSecundaria,
            request.SaudacaoInicial,
            request.Canal,
            request.ModeloIA,
            request.Ativo
        );

        await chatConfigRepository.EditarAsync(chatConfig);
        await chatConfigRepository.SalvarAsync();

        var chatConfigViewModel = new ChatConfigViewModel(
            chatConfig.Id,
            chatConfig.CorPrincipal,
            chatConfig.CorSecundaria,
            chatConfig.SaudacaoInicial,
            chatConfig.Canal,
            chatConfig.ModeloIA,
            chatConfig.Ativo,
            chatConfig.CriadoEm
        );

        return Resultado<ChatConfigViewModel>.Ok(chatConfigViewModel);
    }
}