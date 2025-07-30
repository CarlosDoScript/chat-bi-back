using Chat.Bi.Core.Constantes.Claims;
using Chat.Bi.Core.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Chat.Bi.Infrastructure.Auth;

public class AuthService(
    IConfiguration configuration
    ) : IAuthService
{
    public async Task<(string JwtToken, string RefreshToken)> GerarTokensAsync(Usuario usuario)
    {
        var jwtToken = await GerarJwtTokenAsync(usuario);

        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        usuario.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7));

        return (jwtToken, refreshToken);
    }

    Task<string> GerarJwtTokenAsync(Usuario usuario)
    {
        var issuer = configuration["Jwt:Issuer"];
        var audience = configuration["Jwt:Audience"];
        var key = configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new Claim(TiposClaims.Id,usuario.Id.ToString()),
            new Claim(TiposClaims.Nome,usuario.Nome),
            new Claim(TiposClaims.Admin,usuario.Admin ? "1" : "0"),
            new Claim(TiposClaims.Master,usuario.Master ? "1" : "0"),
            new Claim(TiposClaims.IdEmpresa, usuario.IdEmpresa.ToString())
        };

        if (usuario.IdUsuarioAdmin.HasValue)
            claims.Add(new Claim(TiposClaims.IdUsuarioAdmin, usuario.IdUsuarioAdmin.Value.ToString()));
        else
            claims.Add(new Claim(TiposClaims.IdUsuarioAdmin, string.Empty));

        var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.UtcNow.AddDays(3),
                signingCredentials: credentials,
                claims: claims
            );

        var tokenHandler = new JwtSecurityTokenHandler();

        return Task.FromResult(tokenHandler.WriteToken(token));
    }

    public string GerarSha256Hash(string senha)
    {
        using SHA256 sha256 = SHA256.Create();

        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(bytes[i].ToString("X2"));
        }

        return sb.ToString();
    }

    public string GerarTokenSeguranca(int tamanhoBytes = 32)
    {
        var bytes = new byte[tamanhoBytes];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return WebEncoders.Base64UrlEncode(bytes);
    }
}