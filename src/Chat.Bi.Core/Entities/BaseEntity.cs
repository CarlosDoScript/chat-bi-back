namespace Chat.Bi.Core.Entities;

public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CriadoEm = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public DateTime CriadoEm { get; private set; }
}
