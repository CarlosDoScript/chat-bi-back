using Chat.Bi.Application.Commands.Usuario.LoginContaUsuario;
using FluentValidation;

namespace Chat.Bi.Application.Validators.Usuario;

public class LoginContaUsuarioCommandValidator : AbstractValidator<LoginContaUsuarioCommand>
{
    public LoginContaUsuarioCommandValidator()
    {
        RuleFor(x => x.Email)
          .EmailAddress()
          .WithMessage("E-mail não é válido.");

        RuleFor(x => x.Senha)
            .Must(senha => senha.SenhaValida())
            .WithMessage("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial.");
    }
}
