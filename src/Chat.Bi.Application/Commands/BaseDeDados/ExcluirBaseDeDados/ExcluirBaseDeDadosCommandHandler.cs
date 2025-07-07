namespace Chat.Bi.Application.Commands.BaseDeDados.ExcluirBaseDeDados;

public sealed class ExcluirBaseDeDadosCommandHandler(
    IBaseDeDadosRepository baseDeDadosRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<ExcluirBaseDeDadosCommand, Resultado>
{
    public async Task<Resultado> Handle(ExcluirBaseDeDadosCommand request, CancellationToken cancellationToken)
    {
        var baseDeDados = await baseDeDadosRepository.ObterPorAsync(x => x.Id == request.Id && x.IdEmpresa == usuarioAutenticadoService.ObterIdEmpresa());

        if(baseDeDados is null)
            return Resultado.Falhar("Base de dados não encontrado.");

        await baseDeDadosRepository.RemoverAsync(baseDeDados);
        await baseDeDadosRepository.SalvarAsync();

        return Resultado.Ok();
    }
}