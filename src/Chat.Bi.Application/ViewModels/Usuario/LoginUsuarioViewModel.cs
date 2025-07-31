namespace Chat.Bi.Application.ViewModels.Usuario;

public record LoginUsuarioViewModel(
    string nomeCompleto,
    string email,
    string token,
    string refreshToken,
    string documento,
    DateTime dataNascimento
)
{
}