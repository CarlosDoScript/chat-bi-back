namespace Chat.Bi.Core.Services;

public interface IQueryExecutorService
{
    string TipoBase { get; }
    Task<DataTable> ExecutarQueryAsync(string connectionString, string sql);
}