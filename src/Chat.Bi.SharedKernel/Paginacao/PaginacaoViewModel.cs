namespace Chat.Bi.SharedKernel.Paginacao;

public class PaginacaoViewModel<T>
{
    public PaginacaoViewModel(
       IEnumerable<T> itens,
       int totalRegistros,
       int paginaAtual,
       int tamanhoPagina
    )
    {
        Itens = itens;
        TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanhoPagina);
        TotalRegistros = totalRegistros;
        PaginaAtual = paginaAtual;
    }

    public IEnumerable<T> Itens { get; private set; } = [];
    public int TotalPaginas { get; private set; }
    public int TotalRegistros { get; private set; }
    public int PaginaAtual { get; private set; }
}

public abstract class ConsultaPaginada
{
    public int NumeroPagina { get; init; } = 1;
    public int TamanhoPagina { get; init; } = 10;
    public string? OrdenarPor { get; init; }
    public bool OrdemAscendente { get; init; } = true;
}