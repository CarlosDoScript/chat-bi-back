namespace Chat.Bi.Application.Commands.Usuario.ExcluirUsuario;

public sealed class ExcluirUsuarioCommandHandler(
        IUsuarioRepository usuarioRepository
    ) : IRequestHandler<ExcluirUsuarioCommand, Resultado>
{
    public async Task<Resultado> Handle(ExcluirUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.ObterPorAsync(x => x.Id == request.Id);

        if (usuario is null)
            return Resultado.Falhar("Usuário não encontrado.");

        if (usuario.Admin)
            return Resultado.Falhar("Usuário não pode ser excluído.");

        await usuarioRepository.RemoverAsync(usuario);
        await usuarioRepository.SalvarAsync();

        return Resultado.Ok();
    }
}
