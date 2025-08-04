namespace Chat.Bi.Application.ViewModels.ChatConfig;

public record ChatConfigViewModel(
    int id,
    string corPrincipal,
    string corSecundaria,
    string saudacaoInicial,
    string canal,
    string modeloIA,
    bool ativo,
    string criadoEm
)
{
}