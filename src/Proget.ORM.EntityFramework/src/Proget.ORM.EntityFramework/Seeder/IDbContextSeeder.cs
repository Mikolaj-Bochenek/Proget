namespace Proget.ORM.EntityFramework.Seeder;

public interface IDbContextSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext dbContext);
    Task SaveAsync<TEntity>(TContext dbContext, IEnumerable<TEntity> entities) where TEntity : class;
}
