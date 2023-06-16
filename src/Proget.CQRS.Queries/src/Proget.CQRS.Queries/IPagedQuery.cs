namespace Proget.CQRS.Queries;

public interface IPagedQuery<TResult> : IQuery<TResult>
{
    public int CurrentPage { get; }
    public int PageSize { get; }
    public string? OrderBy { get; }
    public string? SortOrder { get; }
}
