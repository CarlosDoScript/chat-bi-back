namespace Chat.Bi.Application.Validators.BaseDeDados;

public class CriarBaseDeDadosCommandValidator : AbstractValidator<CriarBaseDeDadosCommand>
{
    public CriarBaseDeDadosCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.");

        RuleFor(x => x.Tipo)
            .NotEmpty()
            .WithMessage("Tipo é obrigatório.")
            .Must(canal => TiposBaseDeDados.Todos.Contains(canal))
            .WithMessage("Tipo está inválido.");
        
        RuleFor(x => x.ConnectionString)
            .NotEmpty()
            .WithMessage("ConnectionString é obrigatório.");
        
        RuleFor(x => x.Schema)
            .NotEmpty()
            .WithMessage("Schema é obrigatório.");
        
        RuleFor(x => x.Observacao)
            .NotEmpty()
            .WithMessage("Observação é obrigatório.");
    }
}