namespace Proget.Assets.Amazon.Builders;

public interface IAmazonOptionsBuilder
{
    IAmazonOptionsBuilder WithProjectId(string projectId);
    IAmazonOptionsBuilder WithJsonAuthPath(string jsonAuthPath);
    IAmazonOptionsBuilder WithBucketName(string bucketName);
}