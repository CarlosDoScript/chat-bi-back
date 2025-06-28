using Chat.Bi.Application.Queries.Usuario.ValidarTokenRecuperacaoContaUsuario;
using FluentValidation;

namespace Chat.Bi.Application.Validators.Usuario;

public class ValidarTokenRecuperacaoContaUsuarioQueryValidator : AbstractValidator<ValidarTokenRecuperacaoContaUsuarioQuery>
{
    public ValidarTokenRecuperacaoContaUsuarioQueryValidator()
    {
        RuleFor(x => x.Token)
           .NotNull()
           .NotEmpty()
           .WithMessage("Token é obrigatório.");
    }
}