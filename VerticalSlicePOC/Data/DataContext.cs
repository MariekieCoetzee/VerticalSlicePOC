using Microsoft.EntityFrameworkCore;
using VerticalSlicePOC.Domain;

namespace VerticalSlicePOC.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    public DbSet<GameConsole> Consoles { get; set; }
    public DbSet<Game> Games { get; set; }
}