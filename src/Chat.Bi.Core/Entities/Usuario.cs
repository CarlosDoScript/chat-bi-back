namespace Chat.Bi.Core.Entities;

public class Usuario : BaseEntity
{
    Usuario(
        string nome,
        string email,
        string documento,
        string senhaHash,
        DateTime dataNascimento,
        int idEmpresa,
        bool ativo,
        bool admin
    )
    {
        Nome = nome;
        Email = email;
        Documento = documento;
        SenhaHash = senhaHash;
        DataNascimento = dataNascimento;
        IdEmpresa = idEmpresa;
        Ativo = ativo;
        Admin = admin;
    }

    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Documento { get; private set; }
    public string SenhaHash { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string? RecuperacaoAcessoCodigo { get; private set; }
    public DateTime? RecuperacaoAcessoValidade { get; private set; }
    public bool Ativo { get; private set; }
    public bool Admin { get; private set; }
    public bool Master { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiracao { get; private set; }
    public int IdEmpresa { get; private set; }
    public int? IdUsuarioAdmin { get; private set; }
    public DateTime? UltimoLogin { get; private set; }
    public DateTime? AlteradoEm { get; private set; }

    public virtual Empresa Empresa { get; private set; }

    public static Resultado<Usuario> Criar(
        string nome,
        string email,
        string documento,
        string senhaHash,
        bool admin,
        bool ativo,
        DateTime dataNascimento,
        int idEmpresa
    )
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(nome))
            erros.Add("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            erros.Add("Email é obrigatório.");
        else if (!email.EhEmailValido())
            erros.Add("Email inválido.");

        if (string.IsNullOrWhiteSpace(documento))
            erros.Add("Documento é obrigatório.");
        else if (!documento.EhCpfOuCnpjValido())
            erros.Add("Documento inválido.");

        if (string.IsNullOrWhiteSpace(senhaHash))
            erros.Add("Senha é obrigatória.");

        if (dataNascimento == default)
            erros.Add("Data de nascimento inválida.");

        if (idEmpresa <= 0)
            erros.Add("Empresa é obrigatório.");

        if (erros.Any())
            return Resultado<Usuario>.Falhar(erros);

        var usuario = new Usuario(
            nome.FormatarNome(),
            email,
            documento.FormatarCpfOuCnpj(),
            senhaHash,
            dataNascimento,
            idEmpresa,
            ativo,
            admin
        );

        return Resultado<Usuario>.Ok(usuario);
    }

    public static Resultado<Usuario> CriarConta(
        string nome,
        string email,
        string documento,
        string senhaHash,
        DateTime dataNascimento,
        int idEmpresa
    )
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(nome))
            erros.Add("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email))
            erros.Add("Email é obrigatório.");
        else if (!email.EhEmailValido())
            erros.Add("Email inválido.");

        if (string.IsNullOrWhiteSpace(documento))
            erros.Add("Documento é obrigatório.");
        else if (!documento.EhCpfOuCnpjValido())
            erros.Add("Documento inválido.");

        if (string.IsNullOrWhiteSpace(senhaHash))
            erros.Add("Senha é obrigatória.");

        if (dataNascimento == default)
            erros.Add("Data de nascimento inválida.");

        if (idEmpresa <= 0)
            erros.Add("Empresa é obrigatório.");

        if (erros.Any())
            return Resultado<Usuario>.Falhar(erros);

        var usuario = new Usuario(
            nome.FormatarNome(),
            email,
            documento.FormatarCpfOuCnpj(),
            senhaHash,
            dataNascimento,
            idEmpresa,
            true,
            true
        );

        return Resultado<Usuario>.Ok(usuario);
    }

    public void Alterar(
        string nome,
        string email,
        string documento,
        DateTime dataNascimento,
        bool admin,
        bool ativo
    )
    {
        Nome = nome.FormatarNome();
        Email = email;
        Documento = documento.FormatarCpfOuCnpj();
        DataNascimento = dataNascimento;        
        Admin = admin;
        Ativo = ativo;
    }

    public void SetUltimoLogin()
        => UltimoLogin = DateTime.UtcNow;

    public void SetUsuarioAdmin(int id)
        => IdUsuarioAdmin = id;

    public void SetSenha(string senha)
        => SenhaHash = senha;

    public void SetRecuperacaoAcesso(string codigo)
    {
        RecuperacaoAcessoCodigo = codigo;
        RecuperacaoAcessoValidade = DateTime.UtcNow.AddMinutes(30);
    }

    public void LimparRecuperacaoAcesso()
    {
        RecuperacaoAcessoCodigo = null;
        RecuperacaoAcessoValidade = null;
    }

    public void SetRefreshToken(string refreshToken, DateTime dataExpiracao)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiracao = dataExpiracao;
    }

    public void LimparRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiracao = null;
    }
}