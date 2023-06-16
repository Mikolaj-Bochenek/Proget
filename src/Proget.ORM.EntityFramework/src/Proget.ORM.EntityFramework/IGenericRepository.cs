namespace Proget.ORM.EntityFramework;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
}
