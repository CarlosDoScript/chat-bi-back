using Chat.Bi.Application.ViewModels.Usuario;

namespace Chat.Bi.Application.Queries.Usuario.ObterUsuario;

public sealed class ObterUsuarioQuery : IRequest<Resultado<UsuarioViewModel>>
{
    public int Id { get; set; }
}
