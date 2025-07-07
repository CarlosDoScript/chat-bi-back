using Chat.Bi.Application.ViewModels.BaseDeDados;

namespace Chat.Bi.Application.Queries.BaseDeDados.ListarBaseDeDados;

public sealed class ListarBaseDeDadosQueryHandler(
    IBaseDeDadosRepository baseDeDadosRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<ListarBaseDeDadosQuery, Resultado<PaginacaoViewModel<BaseDeDadosViewModel>>>
{
    public async Task<Resultado<PaginacaoViewModel<BaseDeDadosViewModel>>> Handle(ListarBaseDeDadosQuery request, CancellationToken cancellationToken)
    {
        var (basesDeDados, totalRegistros) = await baseDeDadosRepository.ListarPaginadoAsync(
          x => x.IdEmpresa == usuarioAutenticadoService.ObterIdEmpresa(),
          request.NumeroPagina,
          request.TamanhoPagina,
          request.OrdenarPor ?? nameof(Core.Entities.ChatConfig.CriadoEm),
          request.OrdemAscendente
        );

        var basesDeDadosViewModel = basesDeDados.Select( x => new BaseDeDadosViewModel(
            x.Id,
            x.Nome,
            x.Ativo,
            x.Tipo,
            x.ConnectionStringCriptografada,
            x.SomenteLeitura,
            x.CriadoEm
        ));

        var paginacao = new PaginacaoViewModel<BaseDeDadosViewModel>(basesDeDadosViewModel, totalRegistros, request.NumeroPagina, request.TamanhoPagina);
        return Resultado<PaginacaoViewModel<BaseDeDadosViewModel>>.Ok(paginacao);
    }
}