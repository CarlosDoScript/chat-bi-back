using Chat.Bi.Application.Commands.Usuario.EditarUsuario;
using FluentValidation;

namespace Chat.Bi.Application.Validators.Usuario;

public class EditarUsuarioCommandValidator : AbstractValidator<EditarUsuarioCommand>
{
    public EditarUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome)
          .NotNull()
          .NotEmpty()
          .WithMessage("Nome Completo é obrigatório.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("E-mail não é válido.");

        RuleFor(x => x.Documento)
          .NotNull()
          .NotEmpty()
          .WithMessage("CPF é obrigatório.")
          .Must(doc => doc.EhCpfOuCnpjValido())
          .WithMessage("CPF inválido.");

        RuleFor(x => x.DataNascimento)
            .NotNull()
            .NotEmpty()
            .WithMessage("Data de Nascimento é obrigatório.");
    }
}