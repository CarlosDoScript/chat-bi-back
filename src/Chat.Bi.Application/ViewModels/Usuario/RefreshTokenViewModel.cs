namespace Chat.Bi.Application.ViewModels.Usuario;

public record RefreshTokenViewModel(
    string token,
    string refreshToken,
    DateTime expiraEm
)
{
}
