namespace Chat.Bi.Infrastructure.IA.Factory.Interfaces;

public interface IModelosIaFactory
{
    IModelosIaService GetService(string nomeIa);
}