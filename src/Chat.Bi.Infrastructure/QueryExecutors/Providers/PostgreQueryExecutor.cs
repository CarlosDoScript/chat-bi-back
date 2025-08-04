using Chat.Bi.Core.Constantes.BaseDeDados;
using Npgsql;

namespace Chat.Bi.Infrastructure.QueryExecutors.Executors;

public class PostgreQueryExecutor : IQueryExecutor
{
    public string TipoBase => TiposBaseDeDados.Postgre;

    public async Task<DataTable> ExecutarQueryAsync(string connectionString, string sql)
    {
        var dataTable = new DataTable();

        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        dataTable.Load(reader);

        return dataTable;
    }
}
