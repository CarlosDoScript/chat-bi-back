namespace Chat.Bi.Application.ViewModels.BaseDeDados;

public record BaseDeDadosViewModel(
    int Id,
    string Nome,
    bool Ativo,
    string Tipo,
    string ConnectionStringCriptografada,
    bool SomenteLeitura,
    string Schema,
    string? Observacao,
    DateTime CriadoEm
)
{
    public string CriadoEmFormatado => CriadoEm.ToShortDateString();
}