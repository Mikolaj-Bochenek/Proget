namespace Proget.Assets.File.Builders;

public interface IFileOptionsBuilder : IProgetOptionsBuilder<FileOptions>
{
    IFileOptionsBuilder WithDirectory(string directory);
}