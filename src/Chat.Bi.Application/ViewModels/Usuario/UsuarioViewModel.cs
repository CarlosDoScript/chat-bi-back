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
    public string DataNascimento => dataNascimento.ToShortDateString();
    public string CriadoEm => criadoEm.ToShortDateString();
}