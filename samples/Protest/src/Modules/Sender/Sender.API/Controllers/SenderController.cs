namespace Sender.API.Controllers;

public class SenderController : SenderModuleController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IJWTHandler _jwtHandler;

    public SenderController(ICommandDispatcher commandDispatcher, IJWTHandler jwtHandler)
    {
        _commandDispatcher = commandDispatcher;
        _jwtHandler = jwtHandler;
    }
    
    [HttpPost("send-message")]
    [JWTAuthorize(JWTClaimTypes.Permissions, "Public")] 
    public async Task<IActionResult> CreateMessage(CreateMessage command)
    {
        var id = Guid.NewGuid();

        await _commandDispatcher.SendAsync(command with { Id = id });

        return Ok(id);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignIn command)
    {
        // var claims = new Dictionary<JWTClaimTypes, IEnumerable<string>>
        // {
        //     { JWTClaimTypes.Permissions, command.Type == "Permissions" ? command.Claims.Split(',').ToList() : new List<string>() },
        //     { JWTClaimTypes.Roles, command.Type == "Roles" ? command.Claims.Split(',').ToList() : new List<string>() },
        // };

        var claims = new Dictionary<JWTClaimTypes, IEnumerable<string>>
        {
            { JWTClaimTypes.Permissions, new List<string>() { "Public" } },
        };

        var jsonWebToken = _jwtHandler.GenerateJWT(command.UserId.ToString("N"), "protest", claims);

        await Task.CompletedTask;
        
        return Ok(jsonWebToken);
    }
}
