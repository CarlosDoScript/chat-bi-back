namespace Chat.Bi.Infrastructure.QueryExecutors;

public class QueryExecutorFactory(
        IEnumerable<IQueryExecutorService> executors
    ) : IQueryExecutorFactory
{
    public Resultado<IQueryExecutorService> GetExecutor(string tipoBase)
    {
        var executor = executors.FirstOrDefault(e =>
            e.TipoBase.Equals(tipoBase, StringComparison.InvariantCultureIgnoreCase));

        if (executor is null)
            return Resultado<IQueryExecutorService>.Falhar($"Tipo de base de dados `{tipoBase}` não suportado");

        return Resultado<IQueryExecutorService>.Ok(executor);
    }
}
