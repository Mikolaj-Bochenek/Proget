namespace Proget.Assets.Services;

internal sealed class AssetsService : IAssetsService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AssetsOptions _options;

    public AssetsService(IServiceProvider serviceProvider, AssetsOptions assetsOptions)
    {
        _serviceProvider = serviceProvider;
        _options = assetsOptions;
    }

    public async Task<Asset> UploadAssetAsync(IFormFile formFile)
    {
        var asset = new Asset(formFile);

        if (_options.FileEnabled)
        {
            var fileService = _serviceProvider.GetRequiredService<IFileService>();
            asset = await fileService.UploadAssetAsync(formFile);
        }

        if (_options.GoogleEnabled)
        {
            var googleService = _serviceProvider.GetRequiredService<IGoogleService>();
            asset = await googleService.UploadAssetAsync(formFile);
        }

        return asset;
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        if (_options.FileEnabled)
        {
            var fileService = _serviceProvider.GetRequiredService<IFileService>();
            await fileService.DeleteAssetAsync(asset);
        }

        if (_options.GoogleEnabled)
        {
            var googleService = _serviceProvider.GetRequiredService<IGoogleService>();
            await googleService.DeleteAssetAsync(asset);            
        }
    }
}
