namespace Proget.Builders;

internal sealed class ProgetBuilder : IProgetBuilder
{
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;

    public IServiceCollection Services => _services;
    public IConfiguration Configuration => _configuration;

    private ProgetBuilder(IServiceCollection services, IConfiguration configuration)
    {
        _services = services;
        _configuration = configuration;
    }

    public static ProgetBuilder Create(IServiceCollection services, IConfiguration configuration)
        => new ProgetBuilder(services, configuration);
}
