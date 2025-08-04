namespace Chat.Bi.Infrastructure.IA.QueryProcessor;

public class QueryProcessorService(
    IQueryGeneratorService queryGenerator,
    IQueryExecutorFactory queryExecutorFactory,
    IQueryFormatterService queryFormatter,
    ITipoBaseDeDadosResolver tipoBaseResolver,
    IAppLogger<QueryProcessorService> logger
    ) : IQueryProcessorService
{
    public async Task<Resultado<string>> ProcessarPerguntaAsync(int empresaId, string pergunta)
    {
        var resultadoQuery = await queryGenerator.GerarQueryAsync(empresaId, pergunta);

        if (resultadoQuery.ContemErros)
            return Resultado<string>.Falhar(resultadoQuery.Erros);

        var sql = resultadoQuery.Valor;

        var resultadoTipoBase = await tipoBaseResolver.ObterTipoBaseDeDadosAsync(empresaId);

        if(resultadoTipoBase.ContemErros)
            return Resultado<string>.Falhar(resultadoTipoBase.Erros);

        var resultadoConnectionString = await tipoBaseResolver.ObterConnectionStringAsync(empresaId);

        if (resultadoConnectionString.ContemErros)
            return Resultado<string>.Falhar(resultadoConnectionString.Erros);        
        
        var resultadoExecutor = queryExecutorFactory.GetExecutor(resultadoTipoBase.Valor);

        if (resultadoExecutor.ContemErros)
            return Resultado<string>.Falhar(resultadoExecutor.Erros);
        
        
        DataTable data = null;

        try
        {
            using var table = await resultadoExecutor.Valor.ExecutarQueryAsync(resultadoConnectionString.Valor, sql);
            data = table;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Erro ao executar query para empresa {empresaId}: {sql}");
            return Resultado<string>.Falhar("Ocorreu um erro ao executar a query no banco de dados.");
        }

        var resultadoFormatado = await queryFormatter.FormatarResultadoAsync(data, pergunta, empresaId);

        if (resultadoFormatado.ContemErros)
            return Resultado<string>.Falhar(resultadoFormatado.Erros);

        return Resultado<string>.Ok(resultadoFormatado.Valor);
    }
}