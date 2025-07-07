namespace Chat.Bi.Application.Commands.BaseDeDados.CriarBaseDeDados;

public sealed class CriarBaseDeDadosCommandHandler(
    IBaseDeDadosRepository baseDeDadosRepository,
    IUsuarioAutenticadoService usuarioAutenticadoService
) : IRequestHandler<CriarBaseDeDadosCommand, Resultado<int>>
{
    public async Task<Resultado<int>> Handle(CriarBaseDeDadosCommand request, CancellationToken cancellationToken)
    {
        var resultadoBaseDeDados = Core.Entities.BaseDeDados.Criar(
            request.Nome,
            request.Ativo,
            request.Tipo,
            request.ConnectionString.Criptografar(),
            request.SomenteLeitura,
            usuarioAutenticadoService.ObterIdEmpresa()
        );

        if (resultadoBaseDeDados.ContemErros)
            return Resultado<int>.Falhar(resultadoBaseDeDados.Erros);

        await baseDeDadosRepository.AdicionarAsync(resultadoBaseDeDados.Valor);
        await baseDeDadosRepository.SalvarAsync();

        return Resultado<int>.Ok(resultadoBaseDeDados.Valor.Id);
    }
}
