using Chat.Bi.Application.Commands.Usuario.CriarUsuario;

namespace Chat.Bi.API.Controllers.V1;

[Route("api/v1/usuarios")]
public class UsuariosController(
    Mediator mediator
) : ApiV1Controller
{
    //[HttpGet("listar")]
    //public async Task<IActionResult> ListarAsync([FromQuery] ListarUsuariosQuery query)
    //{
    //    var resultado = await mediator.SendAsync(query);

    //            if (resultado.ContemErros)

    //        return BadRequest(resultado);

    //    return Ok(resultado);
    //}

    //[HttpGet("obter/{id}")]
    //public async Task<IActionResult> ObterAsync(int id)
    //{
    //    var query = new ObterUsuarioQuery() { Id = id };
    //    var resultado = await mediator.SendAsync(query);

    //if (resultado.ContemErros)
    //        return BadRequest(resultado);

    //    return Ok(resultado);
    //}

    [HttpPost("criar")]
    public async Task<IActionResult> CriarAsync([FromBody] CriarUsuarioCommand command)
    {
        var resultado = await mediator.SendAsync(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    //[HttpPut("editar/{id}")]
    //public async Task<IActionResult> EditarAsync(int id, [FromBody] EditarUsuarioCommand command)
    //{
    //    command.SetId(id);
    //    var resultado = await mediator.SendAsync(command);

    //    if (resultado.ContemErros)
    //        return BadRequest(resultado);

    //    return Ok(resultado);
    //}

    //[HttpDelete("excluir/{id}")]
    //public async Task<IActionResult> ExcluirAsync(int id)
    //{
    //    var command = new ExcluirUsuarioCommand() { Id = id };
    //    var resultado = await mediator.SendAsync(command);

    //    if (resultado.ContemErros)
    //        return BadRequest(resultado);

    //    return Ok(resultado);
    //}
}
