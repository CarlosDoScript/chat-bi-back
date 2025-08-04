using Chat.Bi.Core.Constantes.BaseDeDados;

namespace Chat.Bi.Infrastructure.QueryExecutors.Executors;

public class SqlServerQueryExecutor : IQueryExecutor
{
    public string TipoBase => TiposBaseDeDados.SqlServer;

    public async Task<DataTable> ExecutarQueryAsync(string connectionString, string sql)
    {
        return new DataTable();
    }
}