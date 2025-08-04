namespace Chat.Bi.Core.Factories;

public interface IQueryExecutorFactory
{
    Resultado<IQueryExecutor> GetExecutor(string tipoBase);
}
