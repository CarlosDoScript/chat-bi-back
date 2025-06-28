using Chat.Bi.Application.Commands.Usuario.CriarContaUsuario;
using Chat.Bi.Application.Commands.Usuario.EsqueciSenhaContaUsuario;
using Chat.Bi.Application.Commands.Usuario.LoginContaUsuario;
using Chat.Bi.Application.Commands.Usuario.LogoutContaUsuario;
using Chat.Bi.Application.Commands.Usuario.RedefinirSenhaContaUsuario;
using Chat.Bi.Application.Commands.Usuario.RefreshTokenContaUsuario;
using Chat.Bi.Application.Queries.Usuario.ValidarTokenRecuperacaoContaUsuario;

namespace Chat.Bi.API.Controllers.V1;

[Route("api/v1/acesso")]
public class AuthController(
    IMediator mediator
)
 : ApiV1Controller
{
    [HttpPost("criar-conta"),AllowAnonymous]
    public async Task<IActionResult> CriarContaAsync([FromBody] CriarContaUsuarioCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPost("esqueci-senha"),AllowAnonymous]
    public async Task<IActionResult> EsqueciSenhaAsync([FromBody] EsqueciSenhaContaUsuarioCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok();
    }

    [HttpGet("validar-token-recuperacao"),AllowAnonymous]
    public async Task<IActionResult> ValidarTokenRecuperacaoAsync(ValidarTokenRecuperacaoContaUsuarioQuery query)
    {
        var resultado = await mediator.Send(query);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok();
    }

    [HttpPost("redefinir-senha"),AllowAnonymous]
    public async Task<IActionResult> RedefinirSenhaAsync([FromBody] RedefinirSenhaContaUsuarioCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok();
    }

    [HttpPost("login"),AllowAnonymous]
    public async Task<IActionResult> LoginAsync([FromBody] LoginContaUsuarioCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return BadRequest(resultado);

        return Ok(resultado);
    }

    [HttpPost("refresh-token"),AllowAnonymous]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenContaUsuarioCommand command)
    {
        var resultado = await mediator.Send(command);

        if (resultado.ContemErros)
            return Unauthorized(resultado);

        return Ok(resultado);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await mediator.Send(new LogoutContaUsuarioCommand());
        return Ok();
    }
}
