namespace Chat.Bi.Application.Commands.ChatConfig.ExcluirChatConfig;

public sealed class ExcluirChatConfigCommandHandler(
    IChatConfigRepository chatConfigRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<ExcluirChatConfigCommand, Resultado>
{
    public async Task<Resultado> Handle(ExcluirChatConfigCommand request, CancellationToken cancellationToken)
    {
        var chatConfig = await chatConfigRepository.ObterPorAsync(x => x.Id == request.Id && x.IdEmpresa == usuarioAutenticadoService.ObterIdEmpresa());

        if (chatConfig is null)
            return Resultado.Falhar("Configuração do chat não encontrado.");

        await chatConfigRepository.RemoverAsync(chatConfig);
        await chatConfigRepository.SalvarAsync();

        return Resultado.Ok();
    }
}