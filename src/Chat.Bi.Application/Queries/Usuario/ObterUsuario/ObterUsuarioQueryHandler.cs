
using Chat.Bi.Application.ViewModels.Usuario;

namespace Chat.Bi.Application.Queries.Usuario.ObterUsuario;

public sealed class ObterUsuarioQueryHandler(
        IUsuarioRepository usuarioRepository
    ) : IRequestHandler<ObterUsuarioQuery, Resultado<UsuarioViewModel>>
{
    public async Task<Resultado<UsuarioViewModel>> Handle(ObterUsuarioQuery request, CancellationToken cancellationToken)
    {
        var usuario = await usuarioRepository.ObterPorAsync(x => x.Id == request.Id);

        if (usuario is null)
            return Resultado<UsuarioViewModel>.Falhar("Usuário não encontrado.");

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
