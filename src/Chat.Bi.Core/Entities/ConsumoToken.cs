namespace Chat.Bi.Core.Entities;

public class ConsumoToken : BaseEntity
{
    public int TokensUtilizados { get; private set; }
    public int MesReferencia { get; private set; }
    public int IdEmpresa { get; private set; }

    public virtual Empresa Empresa { get; private set; }
}
