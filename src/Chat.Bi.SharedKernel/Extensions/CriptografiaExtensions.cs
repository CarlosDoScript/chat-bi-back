using Chat.Bi.SharedKernel.Configuration;
using System.Security.Cryptography;

namespace Chat.Bi.SharedKernel.Extensions;

public static class CriptografiaExtensions
{
    static CriptografiaSettings _settings;

    public static void Configure(CriptografiaSettings settings)
    {
        _settings = settings;
    }

    public static string Criptografar(this string texto)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_settings.ChavePrivada);
        aes.IV = Encoding.UTF8.GetBytes(_settings.VetorInicializacao);

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
            sw.Write(texto);

        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Descriptografar(this string textoCriptografado)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_settings.ChavePrivada);
        aes.IV = Encoding.UTF8.GetBytes(_settings.VetorInicializacao);

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(Convert.FromBase64String(textoCriptografado));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);

        return sr.ReadToEnd();
    }
}