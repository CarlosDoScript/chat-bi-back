namespace Chat.Bi.Application.ViewModels;

public record RefreshTokenViewModel
{
    public RefreshTokenViewModel(
        string token,
        string refreshToken,
        DateTime expiraEm
    )
    {
        Token = token;
        RefreshToken = refreshToken;
        ExpiraEm = expiraEm;
    }

    public string Token { get; init; }
    public string RefreshToken { get; init; }
    public DateTime ExpiraEm { get; init; }
}
