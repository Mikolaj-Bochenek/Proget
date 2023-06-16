namespace Proget.CQRS.Queries.Paging;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public long TotalResults { get; }

    public bool IsEmpty
        => Items.IsNullOrEmpty();
    public bool IsNotEmpty
        => !IsEmpty;

    private PagedResult()
        => Items = Enumerable.Empty<T>();

    private PagedResult(IEnumerable<T> items,
        int currentPage, int pageSize,
        int totalPages, long totalResults)
    {
        Items = items;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = totalPages;
        TotalResults = totalResults;
    }

    public static PagedResult<T> Create(IEnumerable<T> items,
        int currentPage, int pageSize, int totalPages, long totalResults)
        => new(items, currentPage, pageSize, totalPages, totalResults);

    public static PagedResult<T> Empty => new();
}
