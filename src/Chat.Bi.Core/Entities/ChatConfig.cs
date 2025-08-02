using Chat.Bi.Core.Constantes.ChatConfig;

namespace Chat.Bi.Core.Entities;

public class ChatConfig : BaseEntity
{
    ChatConfig(
        string corPrincipal, 
        string corSecundaria, 
        string saudacaoInicial, 
        string canal, 
        string modeloIA,
        int idEmpresa, 
        bool ativo
    )
    {
        CorPrincipal = corPrincipal;
        CorSecundaria = corSecundaria;
        SaudacaoInicial = saudacaoInicial;
        Canal = canal;
        IdEmpresa = idEmpresa;
        Ativo = ativo;
        ModeloIA = modeloIA;
    }

    public string CorPrincipal { get; private set; }
    public string CorSecundaria { get; private set; }
    public string SaudacaoInicial { get; private set; }
    public string Canal { get; private set; }
    public string ModeloIA { get; private set; }
    public int IdEmpresa { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime? AlteradoEm { get; private set; }   

    public virtual Empresa Empresa { get; private set; }

    public static Resultado<ChatConfig> Criar(
       string corPrincipal,
       string corSecundaria,
       string saudacaoInicial,
       string canal,
       string modeloIA,
       int idEmpresa,
       bool ativo
   )
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(corPrincipal))
            erros.Add("Cor principal é obrigatório.");

        if (string.IsNullOrWhiteSpace(corSecundaria))
            erros.Add("Cor secundaria é obrigatório.");

        if (string.IsNullOrWhiteSpace(saudacaoInicial))
            erros.Add("Saudação inicial é obrigatório.");

        if (string.IsNullOrWhiteSpace(canal) || !ChatConfigCanais.Todos.Contains(canal))
            erros.Add("Canal está inválido.");
        
        if (string.IsNullOrWhiteSpace(modeloIA) || !ChatConfigModelos.Todos.Contains(modeloIA))
            erros.Add("Modelo IA está inválido.");

        if (idEmpresa <= 0)
            erros.Add("Empresa é obrigatório.");

        if (erros.Any())
            return Resultado<ChatConfig>.Falhar(erros);

        var chatConfig = new ChatConfig(
            corPrincipal,
            corSecundaria,
            saudacaoInicial,
            canal,
            modeloIA,
            idEmpresa,
            ativo
        );

        return Resultado<ChatConfig>.Ok(chatConfig);
    }

    public void Alterar(
        string corPrincipal,
        string corSecundaria,
        string saudacaoInicial,
        string canal,
        string modelo,
        bool ativo
    )
    {
        CorPrincipal = corPrincipal;
        CorSecundaria = corSecundaria;
        SaudacaoInicial = saudacaoInicial;
        Canal = canal;
        ModeloIA = modelo;
        Ativo = ativo;
        AlteradoEm = DateTime.UtcNow;
    }
}
