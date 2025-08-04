namespace Chat.Bi.Infrastructure.IA.QueryFormatter;

public class QueryFormatterService(
     IModelosIaFactory factory,
     IModeloIaResolver modeloIaResolver
    ) : IQueryFormatterService
{
    public async Task<Resultado<string>> FormatarResultadoAsync(DataTable data, string perguntaUsuario, int empresaId)
    {
        if (data.Rows.Count == 0)
            return Resultado<string>.Ok("Não foram encontrados resultados para a consulta.");

        var sb = new StringBuilder();
        sb.AppendLine("Resultado da query em formato tabular:\n");

        var colunas = data.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
        sb.AppendLine(string.Join(" | ", colunas));
        sb.AppendLine(new string('-', colunas.Length * 10));

        foreach (DataRow row in data.Rows.Cast<DataRow>().Take(10))
            sb.AppendLine(string.Join(" | ", row.ItemArray.Select(v => v?.ToString() ?? "")));

        var tabela = sb.ToString();

        var prompt = MontarPrompt(tabela,perguntaUsuario);

        var modeloIaService = await modeloIaResolver.ObterModeloIaAsync(empresaId);
        
        var resultadoIaService = factory.GetService(modeloIaService);

        if (resultadoIaService.ContemErros)
            return Resultado<string>.Falhar(resultadoIaService.Erros);

        var resposta = await resultadoIaService.Valor.PerguntarAsync(prompt);

        return Resultado<string>.Ok(resposta);
    }

    string MontarPrompt(string tabela, string perguntaUsuario)
    {
        return $"""
        Você é um assistente de Business Intelligence que interpreta **apenas os dados fornecidos** e gera respostas **claras, amigáveis e úteis**.

        Pergunta do usuário:
        {perguntaUsuario}

        Dados disponíveis (apenas as primeiras linhas da consulta):
        {tabela}

        **Instruções importantes:**
        1. Baseie sua resposta **exclusivamente** nos dados fornecidos acima.
        2. **Não invente nomes, valores ou conclusões** que não estejam na tabela.
        3. Explique os dados de forma **clara e compreensível para um leigo**.
        4. Se houver informações relevantes, destaque **totais, maiores/menores valores ou padrões visíveis**.
        5. Se os dados forem insuficientes para responder completamente, **informe ao usuário de forma educada**.
        6. Utilize **Markdown** se necessário para melhorar a visualização (listas ou tabelas), mas também inclua uma explicação em texto.

        Gere uma resposta amigável, natural e **sem inventar informações**.
        """;
    }

}
