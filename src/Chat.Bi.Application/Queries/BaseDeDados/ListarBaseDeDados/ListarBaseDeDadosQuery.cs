using Chat.Bi.Application.ViewModels.BaseDeDados;

namespace Chat.Bi.Application.Queries.BaseDeDados.ListarBaseDeDados;

public class ListarBaseDeDadosQuery : ConsultaPaginada, IRequest<Resultado<PaginacaoViewModel<BaseDeDadosViewModel>>>
{
}