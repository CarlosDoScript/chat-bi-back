using Chat.Bi.Application.ViewModels.Usuario;

namespace Chat.Bi.Application.Commands.Usuario.LoginContaUsuario;

public sealed class LoginContaUsuarioCommandHandler(
    IAuthService authService,
    IUsuarioRepository usuariosRepository
) : IRequestHandler<LoginContaUsuarioCommand, Resultado<LoginUsuarioViewModel>>
{
    public async Task<Resultado<LoginUsuarioViewModel>> Handle(LoginContaUsuarioCommand command, CancellationToken cancellationToken)
    {
        var senha = authService.GerarSha256Hash(command.Senha);

        var usuario = await usuariosRepository.ObterPorAsync(x => x.SenhaHash == senha && x.Email == command.Email && x.Ativo);

        if (usuario is null)
            return Resultado<LoginUsuarioViewModel>.Falhar("Usuário não encontrado.");

        var tokens = await authService.GerarTokensAsync(usuario);

        var usuarioViewModel = new LoginUsuarioViewModel(
            usuario.Nome,
            command.Email,
            tokens.Token,
            tokens.RefreshToken,
            usuario.Documento,
            usuario.DataNascimento
        );

        usuario.SetUltimoLogin();
        await usuariosRepository.SalvarAsync();

        return Resultado<LoginUsuarioViewModel>.Ok(usuarioViewModel);
    }
}