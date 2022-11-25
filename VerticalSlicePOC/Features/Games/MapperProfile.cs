using AutoMapper;
using VerticalSlicePOC.Domain;

namespace VerticalSlicePOC.Features.Games;

public class MapperProfile :Profile
{
    public MapperProfile()
    {
        CreateMap<Game, AddGameToConsole.GameResult>();
        CreateMap<Game, GetAllGamesForConsole.GameResult>();
        CreateMap<Game, UpdateGameForConsole.UpdateGameResult>();
    }
}