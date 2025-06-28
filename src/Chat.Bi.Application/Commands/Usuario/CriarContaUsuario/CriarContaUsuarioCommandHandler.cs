namespace Chat.Bi.Application.Commands.Usuario.CriarContaUsuario;

public sealed class CriarContaUsuarioCommandHandler(
    IAuthService authService,
    IUsuarioRepository usuarioRepository,
    IEmpresaRepository empresaRepository,
    ChatBiDbContext context
) : IRequestHandler<CriarContaUsuarioCommand, Resultado<int>>
{
    public async Task<Resultado<int>> Handle(CriarContaUsuarioCommand usuarioCommand)
    {
        if (await usuarioRepository.ExisteAsync(usuarioCommand.Documento.FormatarCpfOuCnpj(), usuarioCommand.Email))
            return Resultado<int>.Falhar("Já existe um usuário com este documento ou email.");

        var resultadoEmpresa = Empresa.Criar(
            usuarioCommand.Nome,
            usuarioCommand.Documento,
            usuarioCommand.IdPlano
        );

        if (resultadoEmpresa.ContemErros)
            return Resultado<int>.Falhar(resultadoEmpresa.Erros);

        await using var transaction = await context.Database.BeginTransactionAsync();

        await empresaRepository.AdicionarAsync(resultadoEmpresa.Valor);
        await empresaRepository.SalvarAsync();

        var senhaHash = authService.GerarSha256Hash(usuarioCommand.Senha);

        var resultadoUsuario = Core.Entities.Usuario.CriarConta(
            usuarioCommand.Nome,
            usuarioCommand.Email,
            usuarioCommand.Documento,
            senhaHash,
            usuarioCommand.DataNascimento,
            resultadoEmpresa.Valor.Id
        );

        if (resultadoUsuario.ContemErros)
        {
            await transaction.RollbackAsync();
            return Resultado<int>.Falhar(resultadoUsuario.Erros);
        }

        await usuarioRepository.AdicionarAsync(resultadoUsuario.Valor);
        await usuarioRepository.SalvarAsync();

        await transaction.CommitAsync();

        // HACK: FUTURAMENTE POSSIVELMENTE ENVIAR PARA UMA FILA PARA ENVIAR UM EMAIL PARA NOVO USUARIO CRIADO

        return Resultado<int>.Ok(resultadoUsuario.Valor.Id);
    }
}