namespace Proget.ORM;

public interface IRepository<TEntity, in TIdentifier> where TEntity : IIdentifier<TIdentifier>
{
	Task<TEntity> GetAsync(TIdentifier id);
	Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
	Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

	Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
		TQuery query) where TQuery : PagedBase;

	Task AddAsync(TEntity entity);
	Task UpdateAsync(TEntity entity);
	Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
	Task DeleteAsync(TIdentifier id);
	Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
	Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
	Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
}
