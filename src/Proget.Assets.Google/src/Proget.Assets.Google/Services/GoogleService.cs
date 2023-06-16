namespace Proget.Assets.Google.Services;

internal sealed class GoogleService : IGoogleService
{
    public const string GoogleSection = "google";

    private readonly GoogleOptions _googleOptions;
    private readonly GoogleCredential _googleCredential;
    private readonly StorageClient _storageClient;

    public GoogleService(GoogleOptions googleOptions)
    {
        _googleOptions = googleOptions;
        _googleCredential = GoogleCredential.FromFile(_googleOptions.JsonAuthPath);
        _storageClient = StorageClient.Create(_googleCredential);
    }

    public async Task<Asset> UploadAssetAsync(IFormFile formFile)
    {
        var asset = new Asset(formFile);

        using var memoryStrem = new MemoryStream();
        await formFile.CopyToAsync(memoryStrem);

        var bucket = _googleOptions.BucketName;

        var uploadedAsset = await _storageClient.UploadObjectAsync(bucket, asset.DateSuffixFileName, asset.ContentType, memoryStrem);

        asset.UploadPaths.Add(GoogleSection, uploadedAsset.MediaLink);

        return asset;
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        await _storageClient.DeleteObjectAsync(_googleOptions.BucketName, asset.DateSuffixFileName);
    }
}
