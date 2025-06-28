namespace Chat.Bi.Application.Commands.Usuario.CriarContaUsuario;

public class CriarContaUsuarioCommand : IRequest<Resultado<int>>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Documento { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Senha { get; set; }
    public int IdPlano { get; set; }
}