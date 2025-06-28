using Chat.Bi.Application.Commands.Usuario.CriarContaUsuario;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Chat.Bi.Application.Validators.Usuario;

public class CriarContaUsuarioCommandValidator : AbstractValidator<CriarContaUsuarioCommand>
{
    public CriarContaUsuarioCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório.")
            .EmailAddress()
            .WithMessage("Email inválido.");

        RuleFor(x => x.Documento)
            .NotEmpty()
            .WithMessage("Documento é obrigatório.")
            .Must(doc => doc.EhCpfOuCnpjValido())
            .WithMessage("Documento inválido.");

        RuleFor(x => x.Senha)
            .Must(senha => senha.SenhaValida())
            .WithMessage("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial.");

        RuleFor(x => x.DataNascimento)
            .NotEqual(default(DateTime))
            .WithMessage("Data de nascimento inválida.");

        RuleFor(x => x.IdPlano)
            .NotEmpty()
            .WithMessage("Plano é obrigatório.");
    }  
}
