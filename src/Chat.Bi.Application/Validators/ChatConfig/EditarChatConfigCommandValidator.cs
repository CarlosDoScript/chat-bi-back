using Chat.Bi.Application.Commands.ChatConfig.EditarChatConfig;

namespace Chat.Bi.Application.Validators.ChatConfig;

public class EditarChatConfigCommandValidator : AbstractValidator<EditarChatConfigCommand>
{
    public EditarChatConfigCommandValidator()
    {
        RuleFor(x => x.CorPrincipal)
          .NotEmpty()
          .WithMessage("Cor principal é obrigatório.");

        RuleFor(x => x.CorSecundaria)
            .NotEmpty()
            .WithMessage("Cor secundaria é obrigatório.");

        RuleFor(x => x.SaudacaoInicial)
            .NotEmpty()
            .WithMessage("Saudação inicial é obrigatório.");

        RuleFor(x => x.Canal)
            .NotEmpty()
            .WithMessage("Canal é obrigatório.");
    }
}
