namespace Chat.Bi.Application.Queries.Usuario.ListarUsuario;

public class ListarUsuarioQuery : ConsultaPaginada, IRequest<Resultado<PaginacaoViewModel<UsuarioViewModel>>>
{
}