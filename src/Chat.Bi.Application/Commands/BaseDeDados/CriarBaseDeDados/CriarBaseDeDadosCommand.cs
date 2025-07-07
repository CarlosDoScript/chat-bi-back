namespace Chat.Bi.Application.Commands.BaseDeDados.CriarBaseDeDados;

public class CriarBaseDeDadosCommand : IRequest<Resultado<int>>
{
    public string Nome { get; set; }
    public bool Ativo { get; set; } = true;
    public string Tipo { get; set; }
    public string ConnectionString { get; set; }
    public bool SomenteLeitura { get; set; }
}
