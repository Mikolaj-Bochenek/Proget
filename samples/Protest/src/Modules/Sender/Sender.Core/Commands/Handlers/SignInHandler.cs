using Proget.Auth;
using Proget.Auth.Types;

namespace Sender.Core.Commands.Handlers;

public class SignInHandler : ICommandHandler<SignIn>
{
    private readonly IJWTHandler _jwtHandler;
    
    public SignInHandler(IJWTHandler jwtHandler)
        => _jwtHandler = jwtHandler;

    public async Task HandleAsync(SignIn command, CancellationToken cancellationToken = default)
    {
        var claims = new Dictionary<JWTClaimTypes, IEnumerable<string>>
        {
            { JWTClaimTypes.Permissions, command.Type == "Permissions" ? command.Claims.Split(',').ToList() : new List<string>() },
            { JWTClaimTypes.Roles, command.Type == "Roles" ? command.Claims.Split(',').ToList() : new List<string>() },
        };

        var jsonWebToken = _jwtHandler.GenerateJWT(command.UserId.ToString("N"), "protest", claims);
        
       await Task.CompletedTask;
    }
}