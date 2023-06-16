namespace ModularRecipient.API.Controllers;

public class RecipientController : RecipientModuleController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public RecipientController(IQueryDispatcher queryDispatcher)
        => _queryDispatcher = queryDispatcher;
    
    [HttpGet]
    public async Task<IActionResult> GetMessages(GetMessages query)
    {
        var result = await _queryDispatcher.QueryAsync<IEnumerable<RecipientMessage>>(query);

        return Ok(result);
    }
}
