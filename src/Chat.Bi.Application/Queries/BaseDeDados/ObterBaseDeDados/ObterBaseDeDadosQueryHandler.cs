using Chat.Bi.Application.ViewModels.BaseDeDados;

namespace Chat.Bi.Application.Queries.BaseDeDados.ObterBaseDeDados;

public sealed class ObterBaseDeDadosQueryHandler(
    IBaseDeDadosRepository baseDeDadosRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<ObterBaseDeDadosQuery, Resultado<BaseDeDadosViewModel>>
{
    public async Task<Resultado<BaseDeDadosViewModel>> Handle(ObterBaseDeDadosQuery request, CancellationToken cancellationToken)
    {
        var baseDeDados = await baseDeDadosRepository.ObterPorAsync(x => x.Id == request.Id && x.IdEmpresa == usuarioAutenticadoService.ObterIdEmpresa());

        if (baseDeDados is null)
            return Resultado<BaseDeDadosViewModel>.Falhar("Base de dados não encontrado.");

        var baseDeDadosViewModel = new BaseDeDadosViewModel(
            baseDeDados.Id,
            baseDeDados.Nome,
            baseDeDados.Ativo,
            baseDeDados.Tipo,
            baseDeDados.ConnectionStringCriptografada,
            baseDeDados.SomenteLeitura,
            baseDeDados.Schema,
            baseDeDados.Observacao,
            baseDeDados.CriadoEm.ToShortDateString()
        );

        return Resultado<BaseDeDadosViewModel>.Ok(baseDeDadosViewModel);
    }
}