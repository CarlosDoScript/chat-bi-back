namespace Chat.Bi.Application.Commands.Usuario.EditarUsuario;

public class EditarUsuarioCommand : IRequest<Resultado<UsuarioViewModel>>
{
    internal int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Documento { get; set; }
    public DateTime DataNascimento { get; set; }
    public bool Admin { get; set; }
    public bool Ativo { get; set; }

    public void SetId(int id)
        => Id = id;
}
