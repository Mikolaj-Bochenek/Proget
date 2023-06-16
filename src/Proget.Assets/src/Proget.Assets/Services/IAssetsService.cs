namespace Proget.Assets.Services;

public interface IAssetsService
{
    Task<Asset> UploadAssetAsync(IFormFile formFile);
    Task DeleteAssetAsync(Asset asset); 
}
