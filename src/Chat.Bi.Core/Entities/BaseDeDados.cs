namespace Chat.Bi.Core.Entities;

public class BaseDeDados : BaseEntity
{
    public string Nome { get; private set; }
    public bool Ativo { get; private set; }
    public string Tipo { get; private set; }
    public string ConnectionStringCriptografada { get; private set; }
    public bool SomenteLeitura { get; private set; }
    public int IdEmpresa { get; private set; }
    public DateTime? AlteradoEm { get; private set; }

    public virtual Empresa Empresa { get; private set; }
}
