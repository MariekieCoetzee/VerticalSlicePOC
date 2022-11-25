using System.Data.Entity;
using VerticalSlicePOC.Data;
using VerticalSlicePOC.Domain;

namespace VerticalSlicePOC.Features.Games;

public class GameService : IGameService
{
    private readonly DataContext _context;

    public GameService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Game>> GetAllGamesAsync(int consoleId)
    {
        var result = _context.Games.Where(x => x.ConsoleId == consoleId).AsQueryable();
        return result.ToList();
    }

    public async Task<Game> GetGameAsync(int consoleId, int gameId)
    {
        var result = _context.Games.FirstOrDefault(x => x.ConsoleId == consoleId && x.Id == gameId);
        return result;
    }

    public void AddGameToConsole(int consoleId, Game game)
    {
        game.ConsoleId = consoleId;
        _context.Games.Add(game);
    }

    public void DeleteGame(Game game)
    {
        _context.Games.Remove(game);
    }
}