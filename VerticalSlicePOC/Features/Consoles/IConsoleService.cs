using VerticalSlicePOC.Domain;

namespace VerticalSlicePOC.Features.Consoles;

public interface IConsoleService
{
    Task<IEnumerable<GameConsole>> GetAllConsolesAsync();
    Task<GameConsole> GetConsoleByIdAsync(int consoleId);
}