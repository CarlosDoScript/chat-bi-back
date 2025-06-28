namespace Chat.Bi.Core.Entities;

public class Pergunta : BaseEntity
{
    public string TextoPergunta { get; private set; }
    public string TextoResposta { get; private set; }
    public int TokensConsumidos { get; private set; }
    public DateTime DataHora { get; private set; }
    public int IdUsuario { get; private set; }
    public int? IdBaseDeDados { get; private set; }
    public int? IdIntegracaoApi { get; private set; }

    public virtual Usuario Usuario { get; private set; }
    public virtual BaseDeDados? BaseDeDados { get; private set; }
    public virtual IntegracaoApi? IntegracaoApi { get; private set; }
}
