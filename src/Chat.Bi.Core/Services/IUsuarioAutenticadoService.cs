namespace Chat.Bi.Core.Services;

public interface IUsuarioAutenticadoService
{
    int ObterId();
    int ObterIdEmpresa();
    int ObterIdUsuarioAdmin();
    string ObterNome();
    string ObterEmail();
    public bool EhAdmin();
    public bool EhMaster();
    bool EhAutenticado();
}