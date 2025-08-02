namespace Chat.Bi.Application.Queries.ChatConfig.ListarChatConfig;

public class ListarChatConfigQueryHandler(
    IChatConfigRepository chatConfigRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<ListarChatConfigQuery, Resultado<PaginacaoViewModel<ChatConfigViewModel>>>
{
    public async Task<Resultado<PaginacaoViewModel<ChatConfigViewModel>>> Handle(ListarChatConfigQuery request, CancellationToken cancellationToken)
    {
        var (chatsConfigs, totalRegistros) = await chatConfigRepository.ListarPaginadoAsync(
            x => x.IdEmpresa == usuarioAutenticadoService.ObterIdEmpresa(),
            request.NumeroPagina,
            request.TamanhoPagina,
            request.OrdenarPor ?? nameof(Core.Entities.ChatConfig.CriadoEm),
            request.OrdemAscendente
        );

        var chatsConfigsViewModel = chatsConfigs.Select(x => 
            new ChatConfigViewModel(
                 x.Id,
                 x.CorPrincipal,
                 x.CorSecundaria,
                 x.SaudacaoInicial,
                 x.Canal,
                 x.ModeloIA,
                 x.Ativo,
                 x.CriadoEm
            )
        );

        var paginacao = new PaginacaoViewModel<ChatConfigViewModel>(chatsConfigsViewModel, totalRegistros, request.NumeroPagina, request.TamanhoPagina);
        return Resultado<PaginacaoViewModel<ChatConfigViewModel>>.Ok(paginacao);
    }
}