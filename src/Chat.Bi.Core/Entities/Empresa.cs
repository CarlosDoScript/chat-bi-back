using Chat.Bi.Core.Constantes.Empresa;

namespace Chat.Bi.Core.Entities;

public class Empresa : BaseEntity
{
    Empresa(
        string nome,
        string documento,
        int idPlano,
        string status,
        bool ativo
     )
    {
        Nome = nome;
        Documento = documento;
        IdPlano = idPlano;
        Status = status;
        Ativo = ativo;
    }

    public static Resultado<Empresa> Criar(
        string nome,
        string documento,
        int idPlano
        )
    {
        var erros = new List<string>();
        
        if (string.IsNullOrWhiteSpace(nome))
            erros.Add("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(documento))
            erros.Add("Documento é obrigatório.");
        else if (!documento.EhCpfOuCnpjValido())
            erros.Add("Documento inválido.");

        if (idPlano <= 0)
            erros.Add("Plano é obrigatório.");

        if (erros.Any())
            return Resultado<Empresa>.Falhar(erros);

        var empresa = new Empresa(
            nome.ToSlug(),
            documento.FormatarCpfOuCnpj(),
            idPlano,
            TipoStatusEmpresa.ANDAMENTO,
            true
        );

        return Resultado<Empresa>.Ok(empresa);
    }

    public string Nome { get; private set; }
    public bool Ativo { get; private set; }
    public string Documento { get; private set; }
    public int IdPlano { get; private set; }
    public string Status { get; private set; }
    public DateTime? AlteradoEm { get; private set; }

    public virtual Plano Plano { get; private set; }
}
