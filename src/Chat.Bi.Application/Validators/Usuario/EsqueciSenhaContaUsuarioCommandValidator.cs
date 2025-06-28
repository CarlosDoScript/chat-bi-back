using Chat.Bi.Application.Commands.Usuario.EsqueciSenhaContaUsuario;
using FluentValidation;

namespace Chat.Bi.Application.Validators.Usuario;

public class EsqueciSenhaContaUsuarioCommandValidator : AbstractValidator<EsqueciSenhaContaUsuarioCommand>
{
    public EsqueciSenhaContaUsuarioCommandValidator()
    {
        RuleFor(x => x.Email)
        .EmailAddress()
        .WithMessage("E-mail não é válido.");
    }
}