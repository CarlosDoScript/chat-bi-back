using Chat.Bi.Application.Commands.BaseDeDados.EditarBaseDeDados;

namespace Chat.Bi.Application.Validators.BaseDeDados;

public class EditarBaseDeDadosCommandValidator : AbstractValidator<EditarBaseDeDadosCommand>
{
    public EditarBaseDeDadosCommandValidator()
    {
        RuleFor(x => x.Nome)
         .NotEmpty()
         .WithMessage("Nome é obrigatório.");

        RuleFor(x => x.Tipo)
            .NotEmpty()
            .WithMessage("Tipo é obrigatório.");

        RuleFor(x => x.ConnectionString)
            .NotEmpty()
            .WithMessage("ConnectionString é obrigatório.");

        RuleFor(x => x.ConnectionString)
            .NotEmpty()
            .WithMessage("ConnectionString é obrigatório.");
    }
}
