namespace Proget.Assets.File.Builders;

internal sealed class FileOptionsBuilder : IFileOptionsBuilder
{
    private readonly FileOptions _options = new();

    public IFileOptionsBuilder WithDirectory(string directory)
    {
        _options.Directory = directory;
        return this;
    }

    public FileOptions Build() => _options;
}