namespace Proget.Options;

public interface IProgetOptionsBuilder<TOptions> where TOptions : class, new()
{
    TOptions Build();
}
