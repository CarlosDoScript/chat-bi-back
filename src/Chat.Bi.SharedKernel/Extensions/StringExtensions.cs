using System.Net.Mail;

namespace Chat.Bi.SharedKernel.Extensions;

public static class StringExtensions
{
    public static string ToSlug(this string text, string separator = "-")
    {
        text = text ?? string.Empty;
        separator = separator ?? string.Empty;
        string value = text.Normalize(NormalizationForm.FormD).Trim();
        StringBuilder builder = new StringBuilder();

        foreach (char c in text.ToCharArray())
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                builder.Append(c);
        }

        value = builder.ToString();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        byte[] bytes = Encoding.GetEncoding(1251).GetBytes(value);
        value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", separator);

        return value.ToLowerInvariant();
    }

    public static string RemoverTodosEspacos(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        Span<char> buffer = stackalloc char[input.Length];
        int j = 0;

        foreach (char c in input)
        {
            if (!char.IsWhiteSpace(c))
            {
                buffer[j++] = c;
            }
        }

        return new string(buffer.Slice(0, j));
    }

    public static string RemoverEspacosECaracteresEspeciais(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        Span<char> buffer = stackalloc char[input.Length];
        int j = 0;

        foreach (char c in input)
        {
            if (!char.IsWhiteSpace(c) && c != '-' && c != '.' && c != '/')
            {
                buffer[j++] = c;
            }
        }

        return new string(buffer.Slice(0, j));
    }

    public static string FormatarNome(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        Span<char> buffer = stackalloc char[input.Length];
        bool novoPalavra = true;
        int j = 0;

        foreach (char c in input)
        {
            if (char.IsWhiteSpace(c))
            {
                buffer[j++] = c;
                novoPalavra = true;
            }
            else if (novoPalavra)
            {
                buffer[j++] = char.ToUpper(c);
                novoPalavra = false;
            }
            else
            {
                buffer[j++] = char.ToLower(c);
            }
        }

        return new string(buffer.Slice(0, j));
    }

    /// <summary>
    /// Remove todos os caracteres que não são dígitos (0-9).
    /// </summary>
    public static string SomenteNumeros(this string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            return string.Empty;

        return new string(valor.Where(char.IsDigit).ToArray());
    }

    /// <summary>
    /// Retorna o valor formatado como CPF ou CNPJ, se aplicável.
    /// </summary>
    public static string FormatarCpfOuCnpj(this string valor)
    {
        var documento = valor.SomenteNumeros();

        return documento.Length switch
        {
            11 => documento.FormatarComoCpf(),
            14 => documento.FormatarComoCnpj(),
            _ => documento // retorna como está se não for CPF nem CNPJ
        };
    }

    /// <summary>
    /// Verifica se é um CPF válido em termos de tamanho.
    /// </summary>
    public static bool EhCpf(this string valor)
        => valor.SomenteNumeros().Length == 11;

    /// <summary>
    /// Verifica se é um CNPJ válido em termos de tamanho.
    /// </summary>
    public static bool EhCnpj(this string valor)
        => valor.SomenteNumeros().Length == 14;

    /// <summary>
    /// Formata um CPF para o padrão 000.000.000-00
    /// </summary>
    public static string FormatarComoCpf(this string valor)
    {
        var cpf = valor.SomenteNumeros();
        return cpf.Length == 11 ? Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00") : cpf;
    }

    /// <summary>
    /// Formata um CNPJ para o padrão 00.000.000/0000-00
    /// </summary>
    public static string FormatarComoCnpj(this string valor)
    {
        var cnpj = valor.SomenteNumeros();
        return cnpj.Length == 14 ? Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00") : cnpj;
    }

    public static bool EhCpfOuCnpjValido(this string documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
            return false;

        documento = new string(documento.Where(char.IsDigit).ToArray());

        return documento.Length == 11 ? documento.EhCpfValido()
             : documento.Length == 14 ? documento.EhCnpjValido()
             : false;
    }

    public static bool EhCpfValido(this string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;

        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        var digito = resto < 2 ? 0 : 11 - resto;
        tempCpf += digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        digito = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito.ToString());
    }

    public static bool EhCnpjValido(this string cnpj)
    {
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1) return false;

        var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCnpj = cnpj.Substring(0, 12);
        var soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        var resto = soma % 11;
        var digito = resto < 2 ? 0 : 11 - resto;
        tempCnpj += digito;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        digito = resto < 2 ? 0 : 11 - resto;

        return cnpj.EndsWith(digito.ToString());
    }

    public static bool EhEmailValido(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static bool SenhaValida(this string senha)
    {
        var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

        return regex.IsMatch(senha);
    }
}
