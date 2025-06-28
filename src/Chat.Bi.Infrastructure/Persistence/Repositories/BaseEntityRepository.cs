using System.Linq.Dynamic.Core;

namespace Chat.Bi.Infrastructure.Persistence.Repositories;

public class BaseEntityRepository<TEntidade, TId>(
    DbContext context,
    DbSet<TEntidade> dbSet
    ) : IBaseEntityRepository<TEntidade, TId>, IAsyncDisposable
    where TEntidade : class
    where TId : struct
{
    public async Task<TEntidade> AdicionarAsync(TEntidade entidade, CancellationToken cancellationToken = default)
    {
        await dbSet.AddAsync(entidade, cancellationToken);
        return entidade;
    }

    public async Task<IEnumerable<TEntidade>> AdicionarListaAsync(IEnumerable<TEntidade> entidades, CancellationToken cancellationToken = default)
    {
        await dbSet.AddRangeAsync(entidades, cancellationToken);
        return entidades;
    }

    public async Task<bool> ExisteAsync(Expression<Func<TEntidade, bool>> where, CancellationToken cancellationToken = default)
    {
        return await dbSet
            .AsNoTracking()
            .AnyAsync(where, cancellationToken);
    }

    public async Task<IEnumerable<TEntidade>> ListaAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
    {
        IQueryable<TEntidade> query = dbSet;
        query = IncluirPropriedades(includeProperties, query);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntidade>> ListarEOrdernadosPorAsync<TKey>(Expression<Func<TEntidade, bool>> where, Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
    {
        IQueryable<TEntidade> query = dbSet.Where(where);
        query = IncluirPropriedades(includeProperties, query);
        query = ascendente ? query.OrderBy(ordem) : query.OrderByDescending(ordem);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntidade>> ListarOrdenadosPorAsync<TKey>(Expression<Func<TEntidade, TKey>> ordem, bool ascendente = true, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
    {
        IQueryable<TEntidade> query = dbSet;
        query = IncluirPropriedades(includeProperties, query);
        query = ascendente ? query.OrderBy(ordem) : query.OrderByDescending(ordem);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntidade>> ListarPorAsync(Expression<Func<TEntidade, bool>> where, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
    {
        IQueryable<TEntidade> query = dbSet.Where(where);
        query = IncluirPropriedades(includeProperties, query);
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntidade> ObterPorAsync(Expression<Func<TEntidade, bool>> where, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
    {
        IQueryable<TEntidade> query = dbSet.Where(where);
        query = IncluirPropriedades(includeProperties, query);
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntidade> ObterPorIdAsync(TId id, string nomePropriedadeId = "Id", CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includeProperties)
    {
        IQueryable<TEntidade> query = dbSet;
        query = IncluirPropriedades(includeProperties, query);
        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<TId>(e, nomePropriedadeId).Equals(id), cancellationToken);
    }

    public Task<TEntidade> EditarAsync(TEntidade entidade)
    {
        dbSet.Update(entidade);
        return Task.FromResult(entidade);
    }

    public Task RemoverAsync(TEntidade entidade)
    {
        dbSet.Remove(entidade);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    async Task IBaseEntityRepository<TEntidade, TId>.DisposeAsync()
    {
        await context.DisposeAsync();
    }

    static IQueryable<TEntidade> IncluirPropriedades(Expression<Func<TEntidade, object>>[] includeProperties, IQueryable<TEntidade> query)
    {
        if (includeProperties == null || includeProperties.Length == 0)
            return query;

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query;
    }

    public async Task<(IEnumerable<TEntidade> Itens, int TotalRegistros)> ListarPaginadoAsync(Expression<Func<TEntidade, bool>> filtro, int pagina, int tamanhoPagina, string? ordenarPor = null, bool ascendente = true, CancellationToken cancellationToken = default, params Expression<Func<TEntidade, object>>[] includes)
    {
        IQueryable<TEntidade> query = context.Set<TEntidade>()
            .AsNoTracking()
            .Where(filtro);

        foreach (var include in includes)
            query = query.Include(include);

        var totalRegistros = await query.CountAsync();

        if (!string.IsNullOrWhiteSpace(ordenarPor))
            query = query.OrderBy($"{ordenarPor} {(ascendente ? "asc" : "desc")}");

        var itens = await query
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync(cancellationToken);

        return (itens, totalRegistros);
    }


}

