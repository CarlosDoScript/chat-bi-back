namespace Chat.Bi.Application.ViewModels.Usuario;

public record UsuarioViewModel(
    int id,
    string nome,
    string email,
    string documento, 
    DateTime dataNascimento, 
    bool admin,
    bool ativo,
    DateTime criadoEm
)
{
    public int Id { get; init; } = id;
    public string Nome { get; init; } = nome;
    public string Email { get; init; } = email;
    public string Documento { get; init; } = documento;
    public string DataNascimento { get; init; } = dataNascimento.ToShortDateString();
    public bool Admin { get; init; } = ativo;
    public bool Ativo { get; init; } = true;
    public string CriadoEm { get; set; } = criadoEm.ToShortDateString();
}
