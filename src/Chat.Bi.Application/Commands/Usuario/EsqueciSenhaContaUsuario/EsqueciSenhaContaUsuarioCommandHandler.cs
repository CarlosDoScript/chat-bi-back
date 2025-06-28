using Microsoft.Extensions.Configuration;

namespace Chat.Bi.Application.Commands.Usuario.EsqueciSenhaContaUsuario;

public sealed class EsqueciSenhaContaUsuarioCommandHandler(
    IUsuarioRepository usuariosRepository,
    IAuthService authService,
    IConfiguration configuration
) : IRequestHandler<EsqueciSenhaContaUsuarioCommand, Resultado>
{
    public async Task<Resultado> Handle(EsqueciSenhaContaUsuarioCommand request)
    {
        var usuario = await usuariosRepository.ObterPorAsync(x => x.Email == request.Email && x.Ativo);

        if (usuario is null)
            return Resultado.Ok();

        var token = authService.GerarTokenSeguranca();
        usuario.SetRecuperacaoAcesso(token);

        await usuariosRepository.EditarAsync(usuario);
        await usuariosRepository.SalvarAsync();

        //await emailService.EnviarEmailAsync(new Core.DTOs.EmailRequisicaoDTO(
        //    para: request.Email,
        //    assunto: "Recuperação de Acesso",
        //    templateNome: Templates.recuperar_acesso,
        //    variaveis: new Dictionary<string, string>
        //    {
        //        { "nome", usuario.NomeCompleto.FormatarNome() },
        //        { "url", $"{_appSettings.UrlApi}/validar-token-recuperacao?token={token}" }
        //    }));

        return Resultado.Ok();
    }
}
