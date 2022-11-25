using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VerticalSlicePOC.Data;
using VerticalSlicePOC.Domain;
using VerticalSlicePOC.Features.Consoles;
using Xunit;

namespace VerticalSlicePOCTests;

public class ConsoleServiceTest
{
    private DataContext _context;

    [Fact]
    public async Task WhenGetAllConsoles_ThenConsolesAreReturnedSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("consoleDb1").Options;

        _context = new DataContext(options);

        _context.Consoles.Add(
            new GameConsole
            {
                Id = 1,
                Name = "Xbox Series X",
                Manufacturer = "Microsoft"
            }
        );
        await _context.SaveChangesAsync();

        var consoles = await new ConsoleService(_context).GetAllConsolesAsync();
        
        Assert.NotNull(consoles);
        Assert.True(consoles.Any());
    }

    [Fact]
    public async Task WhenGetConsoleById_ThenSpecificConsoleIsReturned()
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("consoleDb2").Options;
        _context = new DataContext(options);

        _context.Consoles.Add(
            new GameConsole
            {
                Id = 1,
                Name = "Xbox Series X",
                Manufacturer = "Microsoft"
            }
        );

        await _context.SaveChangesAsync();

        var console = await new ConsoleService(_context).GetConsoleByIdAsync(1);

        await _context.SaveChangesAsync();
        
        Assert.NotNull(console);
        Assert.True(console.Id ==1);
    }
}