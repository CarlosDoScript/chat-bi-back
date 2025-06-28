namespace Chat.Bi.Core.Entities;

public class AuthToken
{
    public AuthToken(
        string token,
        string refreshToken
    )
    {
        Token = token;
        RefreshToken = refreshToken;
    }

    public string Token { get; private set; }
    public string RefreshToken { get; private set; }
}