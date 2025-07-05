using Chat.Bi.Application.Commands.ChatConfig.CriarChatConfig;

namespace Chat.Bi.API.Controllers.V1;

[Route("api/v1/chat-config")]
public class ChatConfigController(
    IMediator mediator
) : ApiV1Controller
{
    [HttpPost("criar")]
    public async Task<IActionResult> PostAsync([FromBody] CriarChatConfigCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}
