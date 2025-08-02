namespace Chat.Bi.Core.Services;

public interface IRagContextoService
{
    Task<string> GerarContextoAsync();
}