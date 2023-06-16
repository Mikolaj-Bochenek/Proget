namespace Proget.Assets.Amazon.Builders;

internal sealed class AmazonOptionsBuilder : IAmazonOptionsBuilder
{
    private readonly AmazonOptions _options = new();

    public IAmazonOptionsBuilder WithProjectId(string projectId)
    {
        _options.ProjectId = projectId;
        return this;
    }

    public IAmazonOptionsBuilder WithJsonAuthPath(string jsonAuthPath)
    {
        _options.JsonAuthPath = jsonAuthPath;
        return this;
    }

    public IAmazonOptionsBuilder WithBucketName(string bucketName)
    {
        _options.BucketName = bucketName;
        return this;
    }

    public AmazonOptions Build() => _options;
}