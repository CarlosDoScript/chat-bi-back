namespace Chat.Bi.Application.ViewModels.Usuario;

public record UsuarioViewModel(
    int id,
    string nome,
    string email,
    string documento, 
    string dataNascimento, 
    bool admin,
    bool ativo,
    string criadoEm
)
{
}