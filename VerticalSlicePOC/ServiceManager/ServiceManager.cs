using VerticalSlicePOC.Data;
using VerticalSlicePOC.Features.Consoles;
using VerticalSlicePOC.Features.Games;

namespace VerticalSlicePOC.ServiceManager;

public class ServiceManager :IServiceManager
{
    private readonly DataContext _context;
    private IConsoleService _consoleService;
    private IGameService _gameService;

    public ServiceManager(DataContext context)
    {
        _context = context;
    }

    public IConsoleService Console
    {
        get
        {
            if (_consoleService is null)
                _consoleService = new ConsoleService(_context);
            return _consoleService;
        }
    }

    /// <summary>
    ///   Add update delete games
    /// </summary>
    public IGameService Game
    {
        get
        {
            if (_gameService is null)
                _gameService = new GameService(_context);

            return _gameService;
        }
    }

    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}