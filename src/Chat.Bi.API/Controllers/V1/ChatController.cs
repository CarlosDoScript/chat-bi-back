using Chat.Bi.Application.Queries.Chat.ObterHistoricoChat;

namespace Chat.Bi.API.Controllers.V1;

[Route("api/v1/chat")]
public class ChatController(
    IMediator mediator
) : ApiV1Controller
{
    [HttpGet("historico/{id}")]
    public async Task<IActionResult> ListarAsync(int id)
    {
        var query = new ObterHistoricoChatQuery() { ChatId = id };
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }
}