namespace Chat.Bi.Application.Commands.BaseDeDados.ExcluirBaseDeDados;

public class ExcluirBaseDeDadosCommand : IRequest<Resultado>
{
    public int Id { get; set; }
}
