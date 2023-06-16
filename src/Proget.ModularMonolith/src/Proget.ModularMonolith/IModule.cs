namespace Proget.ModularMonolith;

public interface IModule
{
    string Name { get; }

    void Register(IProgetBuilder builder, IConfiguration configuration);
    void Use(IApplicationBuilder app);
    void Expose(IEndpointRouteBuilder endpoints);
}