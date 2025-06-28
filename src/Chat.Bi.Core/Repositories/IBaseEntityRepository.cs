using System.Linq.Expressions;

namespace Chat.Bi.Core.Repositories;

public interface IBaseEntityRepository<TEntidade, in TId>
    where TEntidade :
    class where TId : struct
{
    Task<TEntidade> AdicionarAsync(TEntidade entidade, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntidade>> AdicionarListaAsync(IEnumerable<TEntidade> entidades, CancellationToken cancellationToken = default);

    Task RemoverAsync(TEntidade entidade);

    Task<TEntidade> EditarAsync(TEntidade entidade);

    Task<bool> ExisteAsync(Expression<Func<TEntidade, bool>> where, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntidade>> ListaAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

    Task<(IEnumerable<TEntidade> Itens, int TotalRegistros)> ListarPaginadoAsync(
    Expression<Func<TEntidade, bool>> filtro, int pagina, int tamanhoPagina, string? ordenarPor = null, bool ascendente = true, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includes);

    Task<IEnumerable<TEntidade>> ListarPorAsync(Expression<Func<TEntidade, bool>> where, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

    Task<IEnumerable<TEntidade>> ListarEOrdernadosPorAsync<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

    Task<IEnumerable<TEntidade>> ListarOrdenadosPorAsync<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

    Task<TEntidade> ObterPorAsync(Expression<Func<TEntidade, bool>> where, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

    Task<TEntidade> ObterPorIdAsync(TId id, string nomePropriedadeId = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties);

    Task SalvarAsync(CancellationToken cancellationToken = default);

    Task DisposeAsync();
}

