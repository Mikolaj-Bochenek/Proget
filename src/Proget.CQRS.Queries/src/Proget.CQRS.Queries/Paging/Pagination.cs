namespace Proget.CQRS.Queries.Paging;

public static class Pagination
{
    public static PagedResult<T> PaginateAsync<T>(this IEnumerable<T> collection, IPagedQuery<PagedResult<T>> query)
        => collection.PaginateAsync(query.CurrentPage, query.PageSize, query.OrderBy, query.SortOrder);

    public static PagedResult<T> PaginateAsync<T>(this IEnumerable<T> collection,
        int currentPage, int pageSize, string? orderBy, string? sortOrder)
    {
        if (!collection.Any())
            return PagedResult<T>.Empty;

        if (pageSize <= 0)
            pageSize = 10;

        if (currentPage <= 0)
            currentPage = 1;

        var totalResults = collection.Count();
        var totalPages = (int)Math.Ceiling((decimal) collection.Count() / pageSize);      

        if (currentPage > totalPages)
            currentPage = totalPages;

        List<T> pagedCollection;
        if (string.IsNullOrWhiteSpace(orderBy))
        {
            pagedCollection = collection.SkipAndTake(currentPage, pageSize).ToList();
        }
        else if (sortOrder is null || sortOrder.ToLowerInvariant() == "asc")
        {
            pagedCollection = collection.AsQueryable().OrderBy(ToLambda<T>(orderBy))
                .SkipAndTake(currentPage, pageSize).ToList();
        }
        else
        {
            pagedCollection = collection.AsQueryable().OrderByDescending(ToLambda<T>(orderBy))
                .SkipAndTake(currentPage, pageSize).ToList();
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

    private static IEnumerable<T> SkipAndTake<T>(this IEnumerable<T> collection, int currentPage, int pageSize)
        => collection.Skip((currentPage - 1) * pageSize).Take(pageSize);
}
