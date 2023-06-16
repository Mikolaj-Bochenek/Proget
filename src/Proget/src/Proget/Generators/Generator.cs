namespace Proget.Generators;

internal sealed class Generator : IGenerator
{
    public Guid GenerateId() => Guid.NewGuid();
}
