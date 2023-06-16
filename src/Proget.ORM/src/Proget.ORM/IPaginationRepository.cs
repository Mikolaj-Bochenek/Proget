namespace Proget.ORM;

public interface IPaginationRepository<TEntity, in TIdentifier> where TEntity : IIdentifier<TIdentifier>
{
    Task<TEntity?> GetAsync(TIdentifier id);
    Task<PagedResult<TEntity>> GetAsync<TQuery>(TQuery query) where TQuery : PagedBase;
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> expression);
    Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> expression, TQuery query)
        where TQuery : PagedBase;
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
}
