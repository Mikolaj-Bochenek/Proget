namespace Proget.Assets.Azure.Builders;

internal sealed class AzureOptionsBuilder : IAzureOptionsBuilder
{
    private readonly AzureOptions _options = new();

    public IAzureOptionsBuilder WithProjectId(string projectId)
    {
        _options.ProjectId = projectId;
        return this;
    }

    public IAzureOptionsBuilder WithJsonAuthPath(string jsonAuthPath)
    {
        _options.JsonAuthPath = jsonAuthPath;
        return this;
    }

    public IAzureOptionsBuilder WithBucketName(string bucketName)
    {
        _options.BucketName = bucketName;
        return this;
    }

    public AzureOptions Build() => _options;

}