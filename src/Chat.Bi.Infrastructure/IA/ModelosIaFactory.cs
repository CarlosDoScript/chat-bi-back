namespace Chat.Bi.Infrastructure.IA;

public class ModelosIaFactory(
    IEnumerable<IModelosIaService> services
    ) : IModelosIaFactory
{
    public Resultado<IModelosIaService> GetService(string nomeIa)
    {
        var service = services.FirstOrDefault(s => s.Nome.Equals(nomeIa,StringComparison.InvariantCultureIgnoreCase));
        
        if(service is null)
        {
            return Resultado<IModelosIaService>.Falhar($"Nenhuma IA registrada com o nome '{nomeIa}'.");
        }
        
        return Resultado<IModelosIaService>.Ok(service);
    }
}