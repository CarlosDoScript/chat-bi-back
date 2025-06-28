namespace Chat.Bi.Application.Commands.Usuario.RedefinirSenhaContaUsuario;

public sealed class RedefinirSenhaContaUsuarioCommandHandler(
    IUsuarioRepository usuariosRepository,
    IAuthService authService
) : IRequestHandler<RedefinirSenhaContaUsuarioCommand, Resultado>
{
    public async Task<Resultado> Handle(RedefinirSenhaContaUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await usuariosRepository.ObterPorAsync(x => x.RecuperacaoAcessoCodigo == request.Token && x.Ativo);

        if (usuario is null || !usuario.RecuperacaoAcessoValidade.HasValue || usuario.RecuperacaoAcessoValidade < DateTime.UtcNow)
            return Resultado.Falhar("Token inválido ou expirado.");

        usuario.SetSenha(authService.GerarSha256Hash(request.NovaSenha));
        usuario.LimparRecuperacaoAcesso();

        await usuariosRepository.EditarAsync(usuario);
        await usuariosRepository.SalvarAsync();

        return Resultado.Ok();
    }
}