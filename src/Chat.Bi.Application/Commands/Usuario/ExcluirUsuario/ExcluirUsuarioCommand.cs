namespace Chat.Bi.Application.Commands.Usuario.ExcluirUsuario;

public class ExcluirUsuarioCommand : IRequest<Resultado>
{
    public int Id { get; set; }
}