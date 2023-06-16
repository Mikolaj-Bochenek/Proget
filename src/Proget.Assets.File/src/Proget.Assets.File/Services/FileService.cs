namespace Proget.Assets.File.Services;

internal sealed class FileService : IFileService
{
    public const string FileDirectory = "Assets";
    public const string FileSection = "file";

    private readonly FileOptions _fileOptions;

    public FileService(FileOptions fileOptions)
        => _fileOptions = fileOptions;

    public async Task<Asset> UploadAssetAsync(IFormFile formFile)
    {
        var asset = new Asset(formFile);

        var directory = _fileOptions.Directory ?? FileDirectory;
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        var assetPath = PathExtensions.Combine(directory, asset.DateSuffixFileName);

        using var fileStream = new FileStream(assetPath, FileMode.Create, FileAccess.Write);
        await formFile.CopyToAsync(fileStream);

        asset.UploadPaths.Add(FileSection, assetPath);

        return asset;
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        var assetPath = asset.UploadPaths[FileSection];

        if (System.IO.File.Exists(assetPath))
            System.IO.File.Delete(assetPath);

        await Task.CompletedTask;
    }
}
