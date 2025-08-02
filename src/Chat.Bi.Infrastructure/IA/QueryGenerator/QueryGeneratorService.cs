namespace Chat.Bi.Infrastructure.IA.QueryGenerator;

public class QueryGeneratorService(
    IModelosIaFactory factory,
    IRagContextoService ragContextoService,
    IModeloIaResolver modeloIaResolver,
    IAppLogger<QueryGeneratorService> logger
) : IQueryGeneratorService
{
    public async Task<Resultado<(string Sql, string? RespostaUsuario)>> GerarQueryAsync(int empresaId, string perguntaUsuario)
    {
        var contexto = await ragContextoService.GerarContextoAsync();        
        var prompt = MontarPrompt(contexto, perguntaUsuario);        
        var modeloIa = await modeloIaResolver.ObterModeloIaAsync(empresaId);

        var resultadoIaService = factory.GetService(modeloIa);

        if(resultadoIaService.ContemErros)
            return Resultado<(string, string?)>.Falhar(resultadoIaService.Erros);

        var respostaIa = await resultadoIaService.Valor.PerguntarAsync(prompt);
        
        var (sql, markdown) = ExtrairSqlEResposta(respostaIa);

        if (string.IsNullOrWhiteSpace(sql))
        {
            logger.LogWarning($"IA não retornou SQL válida para empresa {empresaId}. Pergunta: {perguntaUsuario}");
            return Resultado<(string, string?)>.Falhar("A IA não retornou uma query SQL Válida.");
        }

        if (!SqlEhSomenteSelect(sql))
        {
            logger.LogWarning($"IA gerou SQL proibida para empresa {empresaId}: {sql}");
            return Resultado<(string, string?)>.Falhar("A query gerada pela IA não é permitida, somente SELECT é aceito");
        }

        return Resultado<(string Sql, string? RespostaUsuario)>.Ok((sql, markdown));
    }

    string MontarPrompt(string contexto, string perguntaUsuario)
    {
        return $"""
                Você é um assistente especialista em gerar queries SQL para um sistema de BI.

                Regras:
                1. Sempre gere primeiro a query SQL válida.
                2. Depois da query, sugira como apresentar o resultado ao usuário de forma amigável (ex.: tabela Markdown, lista ou resumo).
                3. Nunca invente colunas ou tabelas que não existam.
                4. Se a pergunta for ambígua, assuma o caso mais conservador.
                5. Não execute a query, apenas gere a instrução SQL e a sugestão de apresentação.
                6. A query SQL deve estar dentro de blocos ```sql``` e a sugestão de resposta em blocos ```markdown```.

                Contexto do banco de dados:
                {contexto}

                Pergunta do usuário:
                {perguntaUsuario}

                Responda com o seguinte formato:

                ```sql
                SUA_QUERY_SQL_AQUI
                ```

                ```markdown
                SUA_RESPOSTA_FORMATADA_AQUI
                ```
                """;
    }

    (string Sql, string? RespostaUsuario) ExtrairSqlEResposta(string respostaIa)
    {
        var sql = Regex.Match(respostaIa, @"```sql\s*(.*?)```", RegexOptions.Singleline)
            .Groups[1].Value.Trim();

        var markdown = Regex.Match(respostaIa, @"```markdown\s*(.*?)```", RegexOptions.Singleline)
            .Groups[1].Value.Trim();

        if (string.IsNullOrWhiteSpace(markdown))
        {
            markdown = "Aqui estão os resultados da sua consulta.";
        }

        return (sql, markdown);
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

        var normalized = sql.TrimStart().ToUpperInvariant();

        if (!normalized.StartsWith("SELECT"))
            return false;

        foreach (var comando in SqlProibidos)
            if (normalized.Contains(comando))
                return false;

        return true;
    }
}