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
    public string DataHora => dataHora.ToShortDateString();
}
