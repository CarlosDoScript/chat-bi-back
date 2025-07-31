using Chat.Bi.Application.ViewModels.BaseDeDados;

namespace Chat.Bi.Application.Commands.BaseDeDados.EditarBaseDeDados;

public class EditarBaseDeDadosCommand : IRequest<Resultado<BaseDeDadosViewModel>>
{
    internal int Id { get; set; }
    public string Nome { get; set; }
    public bool Ativo { get; set; }
    public string Tipo { get; set; }
    public string ConnectionString { get; set; }
    public bool SomenteLeitura { get; set; }
    public string Schema { get; set; }
    public string Observacao { get; set; }

    public void SetId(int id)
    => Id = id;
}