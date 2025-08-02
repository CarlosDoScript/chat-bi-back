namespace Chat.Bi.Infrastructure.IA.Factory.Interfaces;

public interface IModelosIaFactory
{
    Resultado<IModelosIaService> GetService(string nomeIa);
}