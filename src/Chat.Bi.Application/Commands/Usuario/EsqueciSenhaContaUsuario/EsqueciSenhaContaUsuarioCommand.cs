namespace Chat.Bi.Application.Commands.Usuario.EsqueciSenhaContaUsuario;

public class EsqueciSenhaContaUsuarioCommand : IRequest<Resultado>
{
    public string Email { get; set; }
}