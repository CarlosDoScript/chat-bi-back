namespace Chat.Bi.Application.ViewModels.Chat;

public record ChatHistoricoViewModel(
    int id,
    string textoPergunta, 
    string textoResposta, 
    DateTime dataHora, 
    string origem, 
    string status, 
    int? contextoAnteriorId
)
{
    public int Id { get; private set; } = id;
    public string TextoPergunta { get; private set; } = textoPergunta;
    public string TextoResposta { get; private set; } = textoResposta;
    public string DataHora { get; private set; } = dataHora.ToShortDateString();
    public string Origem { get; private set; } = origem;
    public string Status { get; private set; } = status;
    public int? ContextoAnteriorId { get; private set; } = contextoAnteriorId;
}
