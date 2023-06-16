namespace Proget.CQRS.Queries.Paging;

public abstract class PagedQuery<TResult> : IPagedQuery<TResult>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
    public string? SortOrder { get; set; }
}
