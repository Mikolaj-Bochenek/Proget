namespace Proget.ORM.EntityFramework.Repositories;

public class GenericRepositoryWithoutSave<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepositoryWithoutSave(DbContext context)
        => (_context, _dbSet) = (context, context.Set<TEntity>());

    public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        => await _dbSet.FindAsync(id);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public virtual async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> expression)
        => await _dbSet.FirstOrDefaultAsync(expression);

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        => await _dbSet.Where(expression).ToListAsync();

    public virtual async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        => await _dbSet.AddRangeAsync(entities);

    public virtual Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }
}
