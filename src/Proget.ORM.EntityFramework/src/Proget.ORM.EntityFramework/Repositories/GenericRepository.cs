namespace Proget.ORM.EntityFramework.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DbContext context)
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
