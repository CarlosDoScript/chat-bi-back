using Chat.Bi.Application.ViewModels.BaseDeDados;

namespace Chat.Bi.Application.Queries.BaseDeDados.ObterBaseDeDados;

public class ObterBaseDeDadosQuery : IRequest<Resultado<BaseDeDadosViewModel>>
{
    public int Id { get; set; }
}