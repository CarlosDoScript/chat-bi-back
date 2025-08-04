namespace Chat.Bi.Infrastructure.IA.QueryGenerator;

public class QueryGeneratorService(
    IModelosIaFactory factory,
    IRagContextoService ragContextoService,
    IModeloIaResolver modeloIaResolver,
    IAppLogger<QueryGeneratorService> logger
) : IQueryGeneratorService
{
    public async Task<Resultado<string>> GerarQueryAsync(int empresaId, string perguntaUsuario)
    {
        var contexto = await ragContextoService.GerarContextoAsync();
        var prompt = MontarPrompt(contexto.Contexto, perguntaUsuario, contexto.TipoBaseDeDados);
        var modeloIa = await modeloIaResolver.ObterModeloIaAsync(empresaId);

        var resultadoIaService = factory.GetService(modeloIa);

        if (resultadoIaService.ContemErros)
            return Resultado<string>.Falhar(resultadoIaService.Erros);

        var respostaIa = await resultadoIaService.Valor.PerguntarAsync(prompt);

        var sql = ExtrairSqlEResposta(respostaIa);

        if (string.IsNullOrWhiteSpace(sql))
        {
            logger.LogWarning($"IA não retornou SQL válida para empresa {empresaId}. Pergunta: {perguntaUsuario}");
            return Resultado<string>.Falhar("A IA não retornou uma query SQL Válida.");
        }

        if (!SqlEhSomenteSelect(sql))
        {
            logger.LogWarning($"IA gerou SQL proibida para empresa {empresaId}: {sql}");
            return Resultado<string>.Falhar("A query gerada pela IA não é permitida, somente SELECT é aceito");
        }

        return Resultado<string>.Ok(sql);
    }

    string MontarPrompt(string contexto, string perguntaUsuario, string tipoBaseDeDados)
    {
        return $"""
                Você é um assistente especialista em gerar queries SQL.

                Regras:
                1. Sempre gere apenas a query SQL válida **EXCLUSIVAMENTE com SELECT**.
                2. Nunca invente colunas ou tabelas que não existam.
                3. Se a pergunta for ambígua, assuma o caso mais conservador.
                4. Não execute a query, apenas gere a instrução SQL.
                5. A query SQL deve estar dentro de blocos ```sql```.

                Contexto do banco de dados {tipoBaseDeDados}:
                {contexto}

                Pergunta do usuário:
                {perguntaUsuario}

                Responda com o seguinte formato:

                ```sql
                SUA_QUERY_SQL_AQUI
                ```                
                """;
    }

    string ExtrairSqlEResposta(string respostaIa)
    {
        var match = Regex.Match(respostaIa, @"```sql\s*(.*?)```", RegexOptions.Singleline);
        return match.Success ? match.Groups[1].Value.Trim() : respostaIa.Trim();
    }

    static readonly string[] SqlProibidos =
    {
        "DELETE", "DROP", "UPDATE", "INSERT",
        "ALTER", "TRUNCATE", "EXEC", "MERGE", "CREATE"
    };

    bool SqlEhSomenteSelect(string sql)
    {
        if (string.IsNullOrWhiteSpace(sql))
            return false;

        var normalized = sql.Trim().ToUpperInvariant();

        return normalized.StartsWith("SELECT")
            && !normalized.Contains(";")
            && !SqlProibidos.Any(cmd => normalized.Contains(cmd))
            && !normalized.Contains("--")
            && !normalized.Contains("/*")
            && !normalized.Contains("*/");
    }

}