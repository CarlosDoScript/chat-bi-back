using Chat.Bi.Application.ViewModels.Usuario;

namespace Chat.Bi.Application.Commands.Usuario.EditarUsuario;

public sealed class EditarUsuarioCommandHandler(
    IUsuarioRepository usuarioRepository
) : IRequestHandler<EditarUsuarioCommand, Resultado<UsuarioViewModel>>
{
    public async Task<Resultado<UsuarioViewModel>> Handle(EditarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.ObterPorAsync(x => x.Id == request.Id);

        if (usuario is null)
            return Resultado<UsuarioViewModel>.Falhar("Usuário não encontrado.");

        usuario.Alterar(
            request.Nome,
            request.Email,
            request.Documento,
            request.DataNascimento,
            request.Admin,
            request.Ativo
        );

        await usuarioRepository.EditarAsync(usuario);
        await usuarioRepository.SalvarAsync();

        var usuarioViewModel = new UsuarioViewModel(
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.Documento,
            usuario.DataNascimento.ToShortDateString(),
            usuario.Admin,
            usuario.Ativo,
            usuario.CriadoEm.ToShortDateString()
        );

        return Resultado<UsuarioViewModel>.Ok(usuarioViewModel);
    }
}
