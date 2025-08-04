namespace Chat.Bi.Core.Factories;

public interface IQueryExecutorFactory
{
    Resultado<IQueryExecutorService> GetExecutor(string tipoBase);
}
