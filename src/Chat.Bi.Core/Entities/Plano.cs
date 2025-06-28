namespace Chat.Bi.Core.Entities;

public class Plano : BaseEntity
{
    public string Nome { get; private set; }
    public bool Ativo { get; private set; }
    public decimal ValorMensal { get; private set; }
    public int LimiteUsuarios { get; private set; }
    public int LimiteBaseDeDados { get; private set; }
    public int LimiteTokenMensais { get; private set; }
}