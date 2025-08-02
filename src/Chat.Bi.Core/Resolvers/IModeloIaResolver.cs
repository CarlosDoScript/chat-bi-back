namespace Chat.Bi.Core.Resolvers;

public interface IModeloIaResolver
{
    Task<string> ObterModeloIaAsync(int empresaId);
}