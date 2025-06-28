namespace Chat.Bi.Application.Commands.Usuario.RefreshTokenContaUsuario;

public sealed class RefreshTokenContaUsuarioCommandHandler(
IUsuarioRepository usuariosRepository,
IAuthService authService
) : IRequestHandler<RefreshTokenContaUsuarioCommand, Resultado<RefreshTokenViewModel>>
{
    public async Task<Resultado<RefreshTokenViewModel>> Handle(RefreshTokenContaUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await usuariosRepository.ObterPorAsync(x => x.RefreshToken == request.RefreshToken);

        if (usuario is null || usuario.RefreshTokenExpiracao < DateTime.UtcNow)
            return Resultado<RefreshTokenViewModel>.Falhar("Refresh token inválido ou expirado");

        var tokens = await authService.GerarTokensAsync(usuario);

        await usuariosRepository.SalvarAsync();

        var refreshTokenViewModel = new RefreshTokenViewModel(tokens.Token, tokens.RefreshToken, usuario.RefreshTokenExpiracao.Value);

        return Resultado<RefreshTokenViewModel>.Ok(refreshTokenViewModel);
    }
}