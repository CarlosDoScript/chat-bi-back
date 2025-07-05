using Chat.Bi.Application.Commands.ChatConfig.CriarChatConfig;

namespace Chat.Bi.Application.Validators.ChatConfig;

public class CriarChatConfigCommandValidator : AbstractValidator<CriarChatConfigCommand>
{
    public CriarChatConfigCommandValidator()
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
