namespace Chat.Bi.Core.Entities;

public class BaseDeDados : BaseEntity
{
    BaseDeDados(
        string nome,
        bool ativo,
        string tipo, 
        string connectionStringCriptografada,
        bool somenteLeitura,
        int idEmpresa
    )
    {
        Nome = nome;
        Ativo = ativo;
        Tipo = tipo;
        ConnectionStringCriptografada = connectionStringCriptografada;
        SomenteLeitura = somenteLeitura;
        IdEmpresa = idEmpresa;
    }

    public string Nome { get; private set; }
    public bool Ativo { get; private set; }
    public string Tipo { get; private set; }
    public string ConnectionStringCriptografada { get; private set; }
    public bool SomenteLeitura { get; private set; }
    public int IdEmpresa { get; private set; }
    public DateTime? AlteradoEm { get; private set; }

    public virtual Empresa Empresa { get; private set; }

    public static Resultado<BaseDeDados> Criar(
        string nome,
        bool ativo,
        string tipo,
        string connectionStringCriptografada,
        bool somenteLeitura,
        int idEmpresa
    )
    {
        var erros = new List<string>();

        if(string.IsNullOrWhiteSpace(nome))
            erros.Add("Nome é obrigatório.");

        if(string.IsNullOrWhiteSpace(tipo))
            erros.Add("Tipo é obrigatório.");
        
        if(string.IsNullOrWhiteSpace(connectionStringCriptografada))
            erros.Add("ConnectionString criptografada é obrigatório.");
        
        if(idEmpresa <= 0)
            erros.Add("Empresa é obrigatório.");

        if (erros.Any())
            return Resultado<BaseDeDados>.Falhar(erros);

        var baseDeDados = new BaseDeDados(
            nome,
            ativo,
            tipo,
            connectionStringCriptografada,
            somenteLeitura,
            idEmpresa
        );

        return Resultado<BaseDeDados>.Ok(baseDeDados);
    }

    public void Alterar(
        string nome,
        bool ativo,
        string tipo,
        string connectionStringCriptografada,
        bool somenteLeitura,
        int idEmpresa
    )
    {
        Nome = nome;
        Ativo = ativo;
        Tipo = tipo;
        ConnectionStringCriptografada = connectionStringCriptografada;
        SomenteLeitura = somenteLeitura;
        IdEmpresa = idEmpresa;
    }
}