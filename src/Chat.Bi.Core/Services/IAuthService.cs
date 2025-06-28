using Chat.Bi.Core.Entities;

namespace Chat.Bi.Core.Services;

public interface IAuthService
{
    Task<AuthToken> GerarTokensAsync(Usuario usuario);
    string GerarSha256Hash(string senha);
    string GerarTokenSeguranca(int tamanhoBytes = 32);
}
