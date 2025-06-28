namespace Chat.Bi.Application.ViewModels;

public record LoginUsuarioViewModel
{
    public LoginUsuarioViewModel(
        string nomeCompleto,
        string email,
        string token,
        string refreshToken,
        string documento,
        DateTime dataNascimento
    )
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        Token = token;
        Documento = documento;
        DataNascimento = dataNascimento;
        RefreshToken = refreshToken;
    }

    public string NomeCompleto { get; init; }
    public string Token { get; init; }
    public string RefreshToken { get; init; }
    public string Email { get; init; }
    public string Documento { get; init; }
    public DateTime DataNascimento { get; init; }
}