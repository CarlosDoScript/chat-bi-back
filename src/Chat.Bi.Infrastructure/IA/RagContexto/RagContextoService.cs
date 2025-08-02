namespace Chat.Bi.Infrastructure.IA.RagContexto;

public class RagContextoService(
        ChatBiDbContext context
    ) : IRagContextoService
{
    
    public async Task<string> GerarContextoAsync()
    {
        var baseDeDados = await context
            .BaseDeDados
            .FirstOrDefaultAsync(x => x.Ativo);

        if(baseDeDados is null || string.IsNullOrWhiteSpace(baseDeDados.Schema))
            return "Nenhuma base de dados foi encontrada.";
        
        var sb = new StringBuilder();

        sb.AppendLine("Tabelas e colunas disponíveis para consultas (com tipos e observações):\n");
        sb.AppendLine();
        sb.AppendLine(baseDeDados.Schema.Trim());

        if (!string.IsNullOrWhiteSpace(baseDeDados.Observacao))
        {
            sb.AppendLine();
            sb.AppendLine($"Observações gerais da base: {baseDeDados.Observacao}");
        }
        
        return sb.ToString();
    }
}