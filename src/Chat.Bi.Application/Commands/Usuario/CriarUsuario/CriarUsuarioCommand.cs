namespace Chat.Bi.Application.Commands.Usuario.CriarUsuario;

public   class CriarUsuarioCommand : IRequest<Resultado<int>>
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Documento { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Senha { get; set; }
    public bool Admin { get; set; }
    public bool Ativo { get; set; }
}