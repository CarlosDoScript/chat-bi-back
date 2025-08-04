namespace Chat.Bi.Core.Services;

public interface IQueryExecutor
{
    string TipoBase { get; }
    Task<DataTable> ExecutarQueryAsync(string connectionString, string sql);
}