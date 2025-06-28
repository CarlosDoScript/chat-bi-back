namespace Chat.Bi.Application.Commands.Usuario.LogoutContaUsuario;

public sealed class LogoutContaUsuarioCommandHandler(
IUsuarioAutenticadoService usuarioAutenticado,
IUsuarioRepository usuariosRepository
) : IRequestHandler<LogoutContaUsuarioCommand, Resultado>
{
    public async Task<Resultado> Handle(LogoutContaUsuarioCommand request, CancellationToken cancellationToken)
    {        
        var usuario = await usuariosRepository.ObterPorIdAsync(usuarioAutenticado.ObterId(), nameof(Core.Entities.Usuario.Id));

        usuario.LimparRefreshToken();
        await usuariosRepository.SalvarAsync();

        return Resultado.Ok();
    }
}