using Chat.Bi.Application.ViewModels.Usuario;

namespace Chat.Bi.Application.Commands.Usuario.RefreshTokenContaUsuario;

public sealed class RefreshTokenContaUsuarioCommand : IRequest<Resultado<RefreshTokenViewModel>>
{
    public string RefreshToken { get; set; }
}