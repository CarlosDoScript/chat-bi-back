namespace Chat.Bi.Core.Entities;

public class IntegracaoApi : BaseEntity
{
    public string Nome { get; private set; }
    public string Url { get; private set; }
    public string MetodoHttp { get; private set; }
    public string Autenticacao { get; private set; }
    public string SchemaResposta { get; private set; }
    public string CorpoRequisicao { get; private set; }
    public string HeadersJson { get; private set; }
    public bool Ativo { get; private set; }
    public int IdEmpresa { get; private set; }
    public DateTime? AlteradoEm { get; private set; }

    public virtual Empresa Empresa { get; private set; }
}
