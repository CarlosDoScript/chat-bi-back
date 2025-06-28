using Chat.Bi.Application.Commands.Usuario.RedefinirSenhaContaUsuario;
using FluentValidation;

namespace Chat.Bi.Application.Validators.Usuario;

public class RedefinirSenhaContaUsuarioCommandValidator : AbstractValidator<RedefinirSenhaContaUsuarioCommand>
{
    public RedefinirSenhaContaUsuarioCommandValidator()
    {
        RuleFor(x => x.Token)
           .NotNull()
           .NotEmpty()
           .WithMessage("Token é obrigatório.");

        RuleFor(x => x.NovaSenha)
            .Must(senha => senha.SenhaValida())
            .WithMessage("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial.");
    }
}
