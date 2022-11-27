using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSlicePOC.Features.Games.Exceptions;

namespace VerticalSlicePOC.Features.Games;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class GamesController : Controller
{
    private readonly IMediator _mediator;
    
    public GamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Query the database to get a list of games.
    /// </summary>
    /// <param name="consoleId">consoleId : ID for the console</param>
    /// <returns>Returns a list of games available in different consoles.
    /// </returns>
    /// <response code="200">Returns the newly created game in the specified console</response>
    /// <response code="409">When the console does not exist</response>
    [HttpGet(Name = "GetGamesForConsole")]
    public async Task<ActionResult<IEnumerable<AddGameToConsole.GameResult>>> GetGamesForConsole([FromQuery]int consoleId)
    {
        try
        {
            var query = new GetAllGamesForConsole.GetGamesQuery
            {
                ConsoleId = consoleId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (NoConsoleExistsException ex)
        {
            return Conflict(new
                                {
                                    ex.Message
                                });
        }
    }

    /// <summary>
    /// Add a game to the specified console
    /// </summary>
    /// <param name="command">The Name and Publisher of the game</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> AddGame(AddGameToConsole.AddGameCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetGamesForConsole", new { consoleId = result.ConsoleId }, result);
        }
        catch (NoConsoleExistsException ex)
        {
            return Conflict(
                new
                {
                    ex.Message
                }
            );
        }
    }

    /// <summary>
    /// Update game information.
    /// </summary>
    /// <param name="consoleId">consoleId : the console id</param>
    /// <param name="command">Update information : Name of the game and the publisher </param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateGameForConsole(int consoleId, UpdateGameForConsole.UpdateGameCommand command)
    {
        try
        {
            command.ConsoleId = consoleId;
            var result = await _mediator.Send(command);
            return NoContent();
        }
        catch (NoConsoleExistsException ex)
        {
            return Conflict(new { ex.Message });
        }
        catch (NoGameExistsException ex)
        {
            return Conflict(
                new
                {
                    ex.Message,
                    ex.ConsoleId,
                    ex.GameId
                }
            );
        }
    }

    /// <summary>
    /// Delete a game
    /// </summary>
    /// <param name="consoleId">consoleId : The ID of the console</param>
    /// <param name="command">gameID : The ID of the game</param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ActionResult> RemoveGameFromConsole(int consoleId, RemoveGameFromConsole.RemoveGameCommand command)
    {
        try
        {
            command.ConsoleId = consoleId;
            await _mediator.Send(command);
            return NoContent();
        }
        catch (NoConsoleExistsException ex)

        {
            return Conflict(
                new
                {
                    ex.Message
                }
            );
        }
        catch (NoGameExistsException ex)
        {
            return Conflict(
                new
                {
                    ex.Message,
                    ex.ConsoleId,
                    ex.GameId
                }
            );
        }
    }

}