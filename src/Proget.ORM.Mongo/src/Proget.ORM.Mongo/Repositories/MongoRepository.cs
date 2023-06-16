namespace Proget.ORM.Mongo;

internal class MongoRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
	where TEntity : IIdentifier<TIdentifier>
{
	public IMongoCollection<TEntity> Collection { get; }

	public MongoRepository(IMongoDatabase database, string collectionName)
	{
		Collection = database.GetCollection<TEntity>(collectionName);
	}

	public Task<TEntity> GetAsync(TIdentifier id)
		=> GetAsync(e => e.Id!.Equals(id));

	public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
		=> Collection.Find(predicate).SingleOrDefaultAsync();

	public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		=> await Collection.Find(predicate).ToListAsync();

	public Task<PagedResult<TEntity>> BrowseAsync<TQuery>(Expression<Func<TEntity, bool>> predicate,
		TQuery query) where TQuery : PagedBase
		=> Collection.AsQueryable().Where(predicate).PaginateAsync(query);

	public Task AddAsync(TEntity entity)
		=> Collection.InsertOneAsync(entity);

	public Task UpdateAsync(TEntity entity)
		=> UpdateAsync(entity, e => e.Id!.Equals(entity.Id));

	public Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
		=> Collection.ReplaceOneAsync(predicate, entity);

	public Task DeleteAsync(TIdentifier id)
		=> DeleteAsync(e => e.Id!.Equals(id));

	public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
		=> Collection.DeleteOneAsync(predicate);
	
	public Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
		=> Collection.DeleteManyAsync(predicate);

	public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
		=> Collection.Find(predicate).AnyAsync();
}