using System.ComponentModel.DataAnnotations;

namespace Chat.Bi.Application.Queries.Usuario.ValidarTokenRecuperacaoContaUsuario;

public class ValidarTokenRecuperacaoContaUsuarioQuery : IRequest<Resultado>
{
    [Required]
    public string Token { get; set; }
}