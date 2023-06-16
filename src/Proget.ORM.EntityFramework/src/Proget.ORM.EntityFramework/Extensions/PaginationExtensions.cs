namespace Proget.ORM.EntityFramework.Extensions;

public static class PaginationExtensions
{
    public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> collection, PagedBase query)
        => await collection.PaginateAsync(query.CurrentPage, query.PageSize, query.OrderBy, query.SortOrder);

    public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> collection,
        int currentPage, int pageSize, string? orderBy, string? sortOrder)
    {
        if (!await collection.AnyAsync())
            return PagedResult<T>.Empty;

        if (pageSize <= 0)
            pageSize = 10;

        if (currentPage <= 0)
            currentPage = 1;

        var totalResults = await collection.CountAsync();
        var totalPages = (int)Math.Ceiling((decimal) await collection.CountAsync() / pageSize);

        if (currentPage > totalPages)
            currentPage = totalPages;

        List<T> pagedCollection;
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            pagedCollection = await collection.SkipAndTake(currentPage, pageSize).ToListAsync();
        }
        else if (sortOrder is null || sortOrder.ToLowerInvariant() == "asc")
        {
            pagedCollection = await collection.OrderBy(ToLambda<T>(orderBy))
                .SkipAndTake(currentPage, pageSize).ToListAsync();
        }
        else
        {
            pagedCollection = await collection.OrderByDescending(ToLambda<T>(orderBy))
                .SkipAndTake(currentPage, pageSize).ToListAsync();
        }

        return PagedResult<T>.Create(pagedCollection, currentPage, pageSize, totalPages, totalResults);
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var propAsObject = Expression.Convert(property, typeof(object));

        return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
    }

    private static IQueryable<T> SkipAndTake<T>(this IQueryable<T> collection, int currentPage, int pageSize)
        => collection.Skip((currentPage - 1) * pageSize).Take(pageSize);
}
