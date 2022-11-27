using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlicePOC.Features.Consoles;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class ConsolesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    // GET

    public ConsolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Returns all the consoles
    /// </summary>
    /// <returns>{Cons}</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllConsoles.ConsoleResult>>> GetConsolesAsync()
    {
        var result = await _mediator.Send((new GetAllConsoles.GetConsolesQuery()));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}