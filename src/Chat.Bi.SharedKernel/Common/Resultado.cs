namespace Chat.Bi.SharedKernel.Common;

public sealed class Resultado<T>
{
    public bool Sucesso => !Erros.Any();
    public bool ContemErros => Erros.Any();
    public IReadOnlyCollection<string> Erros { get; }
    public T? Valor { get; }

    private Resultado(T? valor, List<string> erros)
    {
        Valor = valor;
        Erros = erros;
    }

    public static Resultado<T> Ok(T valor) => new(valor, new());
    public static Resultado<T> Falhar(params string[] erros) => new(default, erros.ToList());
    public static Resultado<T> Falhar(IEnumerable<string> erros) => new(default, erros.ToList());
}

public sealed class Resultado
{
    public bool Sucesso => !Erros.Any();
    public bool ContemErros => Erros.Any();
    public IReadOnlyCollection<string> Erros { get; }

    private Resultado(List<string> erros)
    {
        Erros = erros;
    }

    public static Resultado Ok() => new(new());
    public static Resultado Falhar(params string[] erros) => new(erros.ToList());
    public static Resultado Falhar(IEnumerable<string> erros) => new(erros.ToList());
}