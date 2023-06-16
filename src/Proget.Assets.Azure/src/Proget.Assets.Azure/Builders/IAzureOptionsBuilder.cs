namespace Proget.Assets.Azure.Builders;

public interface IAzureOptionsBuilder
{
    IAzureOptionsBuilder WithProjectId(string projectId);
    IAzureOptionsBuilder WithJsonAuthPath(string jsonAuthPath);
    IAzureOptionsBuilder WithBucketName(string bucketName);
}