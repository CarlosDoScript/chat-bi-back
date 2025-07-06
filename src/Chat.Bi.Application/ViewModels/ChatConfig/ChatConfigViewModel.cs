namespace Chat.Bi.Application.ViewModels.ChatConfig;

public record ChatConfigViewModel(
    int id,
    string corPrincipal,
    string corSecundaria,
    string saudacaoInicial,
    string canal,
    bool ativo,
    DateTime criadoEm
)
{
    public int Id { get; private set; } = id;
    public string CorPrincipal { get; private set; } = corPrincipal;
    public string CorSecundaria { get; private set; } = corSecundaria;
    public string SaudacaoInicial { get; private set; } = saudacaoInicial;
    public string Canal { get; private set; } = canal;
    public bool Ativo { get; private set; } = ativo;
    public string CriadoEm { get; private set; } = criadoEm.ToShortDateString();
}
