namespace Chat.Bi.Application.Commands.Usuario.CriarUsuario;

public sealed class CriarUsuarioCommandHandler(
        IAuthService authService,
        IUsuarioRepository usuarioRepository,
        IUsuarioAutenticadoService usuarioAutenticadoService
    ) : IRequestHandler<CriarUsuarioCommand, Resultado<int>>
{
    public async Task<Resultado<int>> Handle(CriarUsuarioCommand usuarioCommand, CancellationToken cancellationToken)
    {
        if (await usuarioRepository.ExisteAsync(usuarioCommand.Documento.FormatarCpfOuCnpj(), usuarioCommand.Email))
            return Resultado<int>.Falhar("Já existe usuário com este documento ou email.");

        var senhaHash = authService.GerarSha256Hash(usuarioCommand.Senha);

        var resultadoUsuario = Core.Entities.Usuario.Criar(
            usuarioCommand.Nome,
            usuarioCommand.Email,
            usuarioCommand.Documento,
            senhaHash,
            usuarioCommand.Admin,
            usuarioCommand.Ativo,
            usuarioCommand.DataNascimento,
            usuarioAutenticadoService.ObterIdEmpresa()
        );

        if (resultadoUsuario.ContemErros)
            return Resultado<int>.Falhar(resultadoUsuario.Erros);

        if (usuarioCommand.Admin)
            resultadoUsuario.Valor.SetUsuarioAdmin(usuarioAutenticadoService.ObterIdUsuarioAdmin());

        await usuarioRepository.AdicionarAsync(resultadoUsuario.Valor);
        await usuarioRepository.SalvarAsync();

        // HACK: FUTURAMENTE POSSIVELMENTE ENVIAR PARA UMA FILA PARA ENVIAR UM EMAIL PARA NOVO USUARIO CRIADO

        return Resultado<int>.Ok(resultadoUsuario.Valor.Id);
    }
}