using Chat.Bi.Application.ViewModels.Usuario;

namespace Chat.Bi.Application.Queries.Usuario.ListarUsuario;

public sealed class ListarUsuarioQueryHandler(
        IUsuarioRepository usuarioRepository,
        IUsuarioAutenticadoService usuarioAutenticadoService
    ) : IRequestHandler<ListarUsuarioQuery, Resultado<PaginacaoViewModel<UsuarioViewModel>>>
{
    public async Task<Resultado<PaginacaoViewModel<UsuarioViewModel>>> Handle(ListarUsuarioQuery request, CancellationToken cancellationToken)
    {
        var(usuarios, totalRegistros) = await usuarioRepository.ListarPaginadoAsync(
            x => x.IdUsuarioAdmin == usuarioAutenticadoService.ObterIdUsuarioAdmin(),
            request.NumeroPagina,
            request.TamanhoPagina,
            request.OrdenarPor ?? nameof(Core.Entities.Usuario.Nome),
            request.OrdemAscendente
        );

        var usuariosViewModel = usuarios.Select(x =>
            new UsuarioViewModel(
                x.Id,
                x.Nome,
                x.Email,
                x.Documento,
                x.DataNascimento.ToShortDateString(),
                x.Admin,
                x.Ativo,
                x.CriadoEm.ToShortDateString()
            )
        );

        var paginacao = new PaginacaoViewModel<UsuarioViewModel>(usuariosViewModel, totalRegistros, request.NumeroPagina, request.TamanhoPagina);
        return Resultado<PaginacaoViewModel<UsuarioViewModel>>.Ok(paginacao);
    }
}
