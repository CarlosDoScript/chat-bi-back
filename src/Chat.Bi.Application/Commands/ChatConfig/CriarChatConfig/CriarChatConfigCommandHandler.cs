namespace Chat.Bi.Application.Commands.ChatConfig.CriarChatConfig;

public sealed class CriarChatConfigCommandHandler(
     IChatConfigRepository chatConfigRepository,
     IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<CriarChatConfigCommand, Resultado<int>>
{
    public async Task<Resultado<int>> Handle(CriarChatConfigCommand command, CancellationToken cancellationToken)
    {
        var resultadoCanais = await VerificarExisteChatCriadoCanal(command.Canal);

        if (resultadoCanais.ContemErros)
            return Resultado<int>.Falhar(resultadoCanais.Erros);

        var resultadoChatConfig = Core.Entities.ChatConfig.Criar(
            command.CorPrincipal,
            command.CorSecundaria,
            command.SaudacaoInicial,
            command.Canal,
            usuarioAutenticadoService.ObterIdEmpresa(),
            command.Ativo
        );

        if (resultadoChatConfig.ContemErros)
            return Resultado<int>.Falhar(resultadoChatConfig.Erros);

        await chatConfigRepository.AdicionarAsync(resultadoChatConfig.Valor);
        await chatConfigRepository.SalvarAsync();

        return Resultado<int>.Ok(resultadoChatConfig.Valor.Id);
    }

    async Task<Resultado> VerificarExisteChatCriadoCanal(string canal)
    {
        var idEmpresa = usuarioAutenticadoService.ObterIdEmpresa();

        var existeCanal = await chatConfigRepository.ExisteAsync(x => x.IdEmpresa == idEmpresa && x.Canal == canal);

        if (existeCanal)
            return Resultado.Falhar($"Já existe uma configuração de chat criado para o canal {canal}");

        return Resultado.Ok();
    }
}