namespace Chat.Bi.Application.ViewModels.BaseDeDados;

public record BaseDeDadosViewModel(
    int id,
    string nome,
    bool ativo,
    string tipo,
    string connectionStringCriptografa,
    bool somenteLeitura,
    DateTime criadoEm
)
{
    public int Id { get; private set; } = id;
    public string Nome { get; private set; } = nome;
    public bool Ativo { get; private set; } = ativo;
    public string Tipo { get; private set; } = tipo;
    public string ConnectionStringCriptografada { get; private set; } = connectionStringCriptografa;
    public bool SomenteLeitura { get; private set; } = somenteLeitura;
    public string CriadoEm { get; private set; } = criadoEm.ToShortDateString();
}