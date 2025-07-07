using Chat.Bi.Application.Commands.BaseDeDados.CriarBaseDeDados;
using Chat.Bi.Application.Commands.BaseDeDados.EditarBaseDeDados;
using Chat.Bi.Application.Commands.BaseDeDados.ExcluirBaseDeDados;
using Chat.Bi.Application.Queries.BaseDeDados.ListarBaseDeDados;
using Chat.Bi.Application.Queries.BaseDeDados.ObterBaseDeDados;

namespace Chat.Bi.API.Controllers.V1;

[Route("api/v1/base-dados")]
public class BaseDeDadosController(
    IMediator mediator
) : ApiV1Controller
{
    [HttpGet("listar")]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarBaseDeDadosQuery query)
    {
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)

            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpGet("obter/{id}")]
    public async Task<IActionResult> ObterAsync(int id)
    {
        var query = new ObterBaseDeDadosQuery() { Id = id };
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> PostAsync([FromBody] CriarBaseDeDadosCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPut("editar/{id}")]
    public async Task<IActionResult> EditarAsync(int id, [FromBody] EditarBaseDeDadosCommand command)
    {
        command.SetId(id);
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpDelete("excluir/{id}")]
    public async Task<IActionResult> ExcluirAsync(int id)
    {
        var command = new ExcluirBaseDeDadosCommand() { Id = id };
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}