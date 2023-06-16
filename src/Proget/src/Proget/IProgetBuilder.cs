namespace Proget;

public interface IProgetBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
}
