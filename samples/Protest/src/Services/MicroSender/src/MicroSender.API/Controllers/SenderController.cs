namespace MicroSender.API.Controllers;

[Route("sender/[controller]")]
public class SenderController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public SenderController(ICommandDispatcher commandDispatcher)
        => _commandDispatcher = commandDispatcher;
    
    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessage command)
    {
        var id = Guid.NewGuid();

        await _commandDispatcher.SendAsync(command with { Id = id });

        return Ok(id);
    }
}
