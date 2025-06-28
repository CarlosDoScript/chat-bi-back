namespace Chat.Bi.Application.Commands.Usuario.RedefinirSenhaContaUsuario;

public class RedefinirSenhaContaUsuarioCommand : IRequest<Resultado>
{
    public string Token { get; set; }
    public string NovaSenha { get; set; }
}