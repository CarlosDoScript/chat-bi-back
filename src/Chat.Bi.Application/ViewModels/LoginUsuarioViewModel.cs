namespace Chat.Bi.Application.ViewModels;

public record LoginUsuarioViewModel(
    string nomeCompleto,
    string email,
    string token,
    string refreshToken,
    string documento,
    DateTime dataNascimento
)
{
    public string NomeCompleto { get; init; } = nomeCompleto;
    public string Token { get; init; } = token;
    public string RefreshToken { get; init; } = refreshToken;
    public string Email { get; init; } = email;
    public string Documento { get; init; } = documento;
    public DateTime DataNascimento { get; init; } = dataNascimento;
}