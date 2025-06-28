namespace Chat.Bi.Application.Queries.Usuario.ValidarTokenRecuperacaoContaUsuario;

public sealed class ValidarTokenRecuperacaoContaUsuarioQueryHandler(
    IUsuarioRepository usuariosRepository
) : IRequestHandler<ValidarTokenRecuperacaoContaUsuarioQuery, Resultado>
{
    public async Task<Resultado> Handle(ValidarTokenRecuperacaoContaUsuarioQuery request)
    {
        var usuario = await usuariosRepository.ObterPorAsync(x => x.RecuperacaoAcessoCodigo == request.Token);

        if (usuario is null)
            return Resultado.Falhar("Token inválido ou expirado.");

        if (!usuario.RecuperacaoAcessoValidade.HasValue || usuario.RecuperacaoAcessoValidade < DateTime.UtcNow)
            return Resultado.Falhar("Token expirado.");

        return Resultado.Ok();
    }
}