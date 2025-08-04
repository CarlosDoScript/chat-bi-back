namespace Chat.Bi.Infrastructure.QueryExecutors;

public class QueryExecutorFactory(
        IEnumerable<IQueryExecutor> executors
    ) : IQueryExecutorFactory
{
    public Resultado<IQueryExecutor> GetExecutor(string tipoBase)
    {
        var executor = executors.FirstOrDefault(e =>
            e.TipoBase.Equals(tipoBase, StringComparison.InvariantCultureIgnoreCase));

        if (executor is null)
            return Resultado<IQueryExecutor>.Falhar($"Tipo de base de dados `{tipoBase}` não suportado");

        return Resultado<IQueryExecutor>.Ok(executor);
    }
}
