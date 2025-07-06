using Chat.Bi.Application.Commands.ChatConfig.CriarChatConfig;
using Chat.Bi.Application.Commands.ChatConfig.EditarChatConfig;
using Chat.Bi.Application.Commands.ChatConfig.ExcluirChatConfig;
using Chat.Bi.Application.Queries.ChatConfig.ListarChatConfig;
using Chat.Bi.Application.Queries.ChatConfig.ObterChatConfig;

namespace Chat.Bi.API.Controllers.V1;

[Route("api/v1/chat-config")]
public class ChatConfigController(
    IMediator mediator
) : ApiV1Controller
{
    [HttpGet("listar")]
    public async Task<IActionResult> ListarAsync([FromQuery] ListarChatConfigQuery query)
    {
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)

            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpGet("obter/{id}")]
    public async Task<IActionResult> ObterAsync(int id)
    {
        var query = new ObterChatConfigQuery() { Id = id };
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> PostAsync([FromBody] CriarChatConfigCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPut("editar/{id}")]
    public async Task<IActionResult> EditarAsync(int id, [FromBody] EditarChatConfigCommand command)
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
        var command = new ExcluirChatConfigCommand() { Id = id };
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}
