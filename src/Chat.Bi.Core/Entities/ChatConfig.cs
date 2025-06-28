namespace Chat.Bi.Core.Entities;

public class ChatConfig : BaseEntity
{
    public string CorPrincipal { get; private set; }
    public string CorSecundaria { get; private set; }
    public string SaudacaoInicial { get; private set; }
    public string Canal { get; private set; }
    public int IdEmpresa { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime? AlteradoEm { get; private set; }

    public virtual Empresa Empresa { get; private set; }
}
