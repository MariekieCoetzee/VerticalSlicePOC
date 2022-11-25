using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VerticalSlicePOC.Data;
using VerticalSlicePOC.Domain;
using VerticalSlicePOC.Features.Games;
using Xunit;

namespace VerticalSlicePOCTests;

public class GameServiceTest
{
    private DataContext _context;
    
    [Fact]
    public async Task WhenGameIsAdded_GameCreatedSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("gameDB1")
            .Options;

        _context = new DataContext(options);

        _context.Consoles.Add(
            new GameConsole
            {
                Id = 1,
                Name = "Xbox Series x",
                Manufacturer = "Microsoft"
            }
        );
        
        new GameService(_context).AddGameToConsole(1,new Game{ Id= 10, Name = "Fighting Simulator", Publisher = "Best Fights Cop"});

        await _context.SaveChangesAsync();
        
        Assert.True(await _context.Games.FirstOrDefaultAsync() is not null);
    }

    [Fact]
    public async Task WhenGetAllGames_ThenGamesAreReturnedSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("gameDb2").Options;

        _context = new DataContext(options);

        _context.Consoles.Add(
            new GameConsole
            {
                Id = 1,
                Name = "Xbox Series x",
                Manufacturer = "Microsoft"
            }
        );

        new GameService(_context).AddGameToConsole(
            1, new Game { Id = 10, Name = "Fighting Simulator", Publisher = "Best Fights Cop" }
        );

        await _context.SaveChangesAsync();

        var games = await new GameService(_context).GetAllGamesAsync(1);
        
        Assert.NotNull(games);
        
        Assert.True(games.Any());
    }

    [Fact]
    public async Task WhenGetGameByID_ThenSpecifiedGameIsReturnedSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("gameDB3").Options;

        _context = new DataContext(options);

        _context.Consoles.Add(
            new GameConsole
            {
                Id = 1,
                Name = "Xbox Series x",
                Manufacturer = "Microsoft"
            }
        );
        new GameService(_context).AddGameToConsole(1, new Game
            {
                Id = 12, Name = "Running Simulator", Publisher = "Best Runs Cop"
            }
        );
        await _context.SaveChangesAsync();

        var game = await new GameService(_context).GetGameAsync(1, 12);
        
        Assert.NotNull(game);
        
        Assert.True(game.Id is 12);
    }

    [Fact]
    public async Task WhenDeletingAGame_ThenSpecifiedGameIsDeletedSuccessfully()
    {
        var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("gameDB4").Options;

        var context = new DataContext(options);

        context.Consoles.Add(
            new GameConsole
            {
                Id = 1,
                Name = "Xbox Series x",
                Manufacturer = "Microsoft"
            }
        );
        var newGame = new Game { Id = 13, Name = "Game Dev Simulator", Publisher = "Worst Games Corp" };
        
        new GameService(context).AddGameToConsole(1, newGame);

        await context.SaveChangesAsync();
        
        new GameService(context).DeleteGame(newGame);
        await context.SaveChangesAsync();

        var deletedGame = await context.Games.FirstOrDefaultAsync(x => x.Id == 13);
        
        Assert.Null(deletedGame);
    }
    
}