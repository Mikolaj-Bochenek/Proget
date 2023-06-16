namespace Proget.Assets.Google.Builders;

public interface IGoogleOptionsBuilder : IProgetOptionsBuilder<GoogleOptions>
{
    IGoogleOptionsBuilder WithJsonAuthPath(string jsonAuthPath);
    IGoogleOptionsBuilder WithBucketName(string bucketName);
}