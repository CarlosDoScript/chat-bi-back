using Chat.Bi.Application.ViewModels.Usuario;

namespace Chat.Bi.Application.Commands.Usuario.LoginContaUsuario;

public class LoginContaUsuarioCommand : IRequest<Resultado<LoginUsuarioViewModel>>
{
    public string Email { get; set; }
    public string Senha { get; set; }
}