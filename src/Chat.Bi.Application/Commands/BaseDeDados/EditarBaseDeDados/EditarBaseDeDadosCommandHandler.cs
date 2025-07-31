using Chat.Bi.Application.ViewModels.BaseDeDados;

namespace Chat.Bi.Application.Commands.BaseDeDados.EditarBaseDeDados;

public sealed class EditarBaseDeDadosCommandHandler(
    IBaseDeDadosRepository baseDeDadosRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<EditarBaseDeDadosCommand, Resultado<BaseDeDadosViewModel>>
{
    public async Task<Resultado<BaseDeDadosViewModel>> Handle(EditarBaseDeDadosCommand request, CancellationToken cancellationToken)
    {
        var idEmpresa = usuarioAutenticadoService.ObterIdEmpresa();
        var baseDeDados = await baseDeDadosRepository.ObterPorAsync(x => x.Id == request.Id && x.IdEmpresa == idEmpresa);

        if (baseDeDados is null)
            return Resultado<BaseDeDadosViewModel>.Falhar("Base de dados não encontrado.");

        baseDeDados.Alterar(
            request.Nome,
            request.Ativo,
            request.Tipo,
            request.ConnectionString.Criptografar(),
            request.SomenteLeitura,
            idEmpresa,
            request.Schema,
            request.Observacao
        );

        await baseDeDadosRepository.EditarAsync(baseDeDados);
        await baseDeDadosRepository.SalvarAsync();

        var baseDeDadosViewModel = new BaseDeDadosViewModel(
            baseDeDados.Id,
            baseDeDados.Nome,
            baseDeDados.Ativo,
            baseDeDados.Tipo,
            baseDeDados.ConnectionStringCriptografada,
            baseDeDados.SomenteLeitura,
            baseDeDados.Schema,
            baseDeDados.Observacao,
            baseDeDados.CriadoEm
        );

        return Resultado<BaseDeDadosViewModel>.Ok(baseDeDadosViewModel);
    }
}
