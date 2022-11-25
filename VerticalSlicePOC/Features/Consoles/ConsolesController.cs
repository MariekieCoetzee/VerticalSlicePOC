using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSlicePOC.Features.Consoles;

[Route("api/[controller]")]
[ApiController]
public class ConsolesController : ControllerBase
{
    private readonly IMediator _mediator;
    
    // GET

    public ConsolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

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