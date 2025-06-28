namespace Chat.Bi.Core.Entities;

public class Pagamento : BaseEntity
{
    public DateTime DataPagamento { get; private set; }
    public decimal Valor { get; private set; }
    public string Status { get; private set; }
    public string Identificador { get; private set; }
    public int IdEmpresa { get; private set; }
    public DateTime? AlteradoEm { get; private set; }

    public virtual Empresa Empresa { get; private set; }
}