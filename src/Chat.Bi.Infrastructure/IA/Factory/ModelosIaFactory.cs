namespace Chat.Bi.Infrastructure.IA.Factory;

public class ModelosIaFactory(
    IEnumerable<IModelosIaService> services
    ) : IModelosIaFactory
{
    public IModelosIaService GetService(string nomeIa)
    {
        var service = services.FirstOrDefault(s => s.Nome.Equals(nomeIa,StringComparison.InvariantCultureIgnoreCase));
        
        if(service is null)
            throw new InvalidOperationException($"Nenhuma IA registrada com o nome '{nomeIa}'.");
        
        return service;
    }
}