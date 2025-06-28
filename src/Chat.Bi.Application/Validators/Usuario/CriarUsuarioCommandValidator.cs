using Chat.Bi.Application.Commands.Usuario.CriarUsuario;
using FluentValidation;

namespace Chat.Bi.Application.Validators.Usuario;

public class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
{
    public CriarUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome)
         .NotEmpty().WithMessage("Nome é obrigatório.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .Must(email => email.EhEmailValido()).WithMessage("Email inválido.");

        RuleFor(x => x.Documento)
            .NotEmpty().WithMessage("Documento é obrigatório.")
            .Must(doc => doc.EhCpfOuCnpjValido()).WithMessage("Documento inválido.");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória.");

        RuleFor(x => x.DataNascimento)
            .NotEqual(default(DateTime)).WithMessage("Data de nascimento inválida.");
    }
}
