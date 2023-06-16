namespace Proget.Assets.Google.Builders;

internal sealed class GoogleOptionsBuilder : IGoogleOptionsBuilder
{
    private readonly GoogleOptions _options = new();

    public IGoogleOptionsBuilder WithJsonAuthPath(string jsonAuthPath)
    {
        _options.JsonAuthPath = jsonAuthPath;
        return this;
    }

    public IGoogleOptionsBuilder WithBucketName(string bucketName)
    {
        _options.BucketName = bucketName;
        return this;
    }

    public GoogleOptions Build() => _options;

}