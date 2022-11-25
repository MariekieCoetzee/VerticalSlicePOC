using VerticalSlicePOC.Data;
using VerticalSlicePOC.Domain;

namespace VerticalSlicePOC.Features.Consoles;

public class ConsoleService :IConsoleService
{
    private readonly DataContext _context;

    public ConsoleService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GameConsole>> GetAllConsolesAsync()
    {
        var results= _context.Consoles.OrderBy(x => x.Id)
                   .AsQueryable();
        return results.ToList();
    }

    public async Task<GameConsole> GetConsoleByIdAsync(int consoleId)
    {
        var result = _context.Consoles.FirstOrDefault(x => x.Id == consoleId);
        return result;
    }
}