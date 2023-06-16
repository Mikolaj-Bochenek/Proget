namespace Proget.Assets.Models;

public record Asset
{
    public Guid Id { get; init; }
    public DateTime UploadDate { get; init; }
    public long Size { get; init; }
    public string ContentType { get; init; }
    public string ContentDisposition { get; init; }
    public string Name { get; init; }
    public string FileName { get; init; }
    public string? Extension { get; init; }
    public string DateSuffixName { get; init; }
    public string DateSuffixFileName { get; init; }
    public IDictionary<string, string> UploadPaths { get; init; } = new Dictionary<string, string>();

    public Asset(IFormFile formFile)
    {
        Id = Guid.NewGuid();
        UploadDate = DateTime.UtcNow;
        Size = formFile.Length;
        ContentType = formFile.ContentType;
        ContentDisposition = formFile.ContentDisposition;
        Name = Path.GetFileNameWithoutExtension(formFile.FileName);
        FileName = formFile.FileName;
        Extension = PathExtensions.GetExtensionWithoutDot(formFile.FileName);
        DateSuffixName = PathExtensions.GetFileNameWithDateTimeFileSuffix(Name, UploadDate);
        DateSuffixFileName = PathExtensions.CombineExtension(DateSuffixName, Extension);
    }
}
