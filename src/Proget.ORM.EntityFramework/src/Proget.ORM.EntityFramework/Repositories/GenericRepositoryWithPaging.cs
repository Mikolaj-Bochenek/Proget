namespace Proget.ORM.EntityFramework.Repositories;

public class GenericRepositoryWithPaging<TEntity> : IGenericRepositoryWithPaging<TEntity> where TEntity : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepositoryWithPaging(DbContext context)
        => (_context, _dbSet) = (context, context.Set<TEntity>());

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public virtual async Task<PagedResult<TEntity>> GetAllAsync<TQuery>(TQuery query) where TQuery : PagedBase
        => await _dbSet.PaginateAsync(query);

    public virtual async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression)
        => await _dbSet.FirstOrDefaultAsync(expression);

    public virtual async Task<PagedResult<TEntity>> FindAsync<TQuery>(
        Expression<Func<TEntity, bool>> expression, TQuery query) where TQuery : PagedBase
            => await _dbSet.Where(expression).PaginateAsync(query);

    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }
}
