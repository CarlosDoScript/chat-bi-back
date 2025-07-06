namespace Chat.Bi.Application.ViewModels.Usuario;

public record RefreshTokenViewModel(
    string token,
    string refreshToken,
    DateTime expiraEm
)
{
    public string Token { get; init; } = token;
    public string RefreshToken { get; init; } = refreshToken;
    public DateTime ExpiraEm { get; init; } = expiraEm;
}
