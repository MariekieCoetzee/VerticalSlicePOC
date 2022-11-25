using VerticalSlicePOC.Features.Consoles;
using VerticalSlicePOC.Features.Games;

namespace VerticalSlicePOC.ServiceManager;

public interface IServiceManager
{
    IConsoleService Console { get; }
    IGameService Game { get; }
    Task SaveAsync();
}