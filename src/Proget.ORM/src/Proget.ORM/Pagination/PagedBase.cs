namespace Proget.ORM;

public abstract class PagedBase
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public string? OrderBy { get; set; }
    public string? SortOrder { get; set; }
}
