namespace Proget.ORM.EntityFramework;

public interface IGenericRepositoryWithPaging<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<PagedResult<TEntity>> GetAllAsync<TQuery>(TQuery query) where TQuery : PagedBase;
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression);
    Task<PagedResult<TEntity>> FindAsync<TQuery>(Expression<Func<TEntity, bool>> expression, TQuery query)
        where TQuery : PagedBase;
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
}
