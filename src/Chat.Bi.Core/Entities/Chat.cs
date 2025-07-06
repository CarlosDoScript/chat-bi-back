namespace Chat.Bi.Core.Entities;

public class Chat : BaseEntity
{
    public string TextoPergunta { get; private set; }
    public string TextoResposta { get; private set; }
    public int TokensConsumidos { get; private set; }
    public DateTime DataHora { get; private set; }
    public string Origem { get; private set; }
    public string Status { get; private set; }
    public int DuracaoRespostaMs { get; private set; }
    public int IdUsuario { get; private set; }
    public int? IdBaseDeDados { get; private set; }
    public int? IdIntegracaoApi { get; private set; }
    public int? ContextoAnteriorId { get; private set; }

    public virtual Usuario Usuario { get; private set; }
    public virtual BaseDeDados? BaseDeDados { get; private set; }
    public virtual IntegracaoApi? IntegracaoApi { get; private set; }
    public virtual Chat? ContextoAnterior { get; private set; }
    public virtual ICollection<Chat> RespostasFilhas { get; private set; } = new List<Chat>();
}
