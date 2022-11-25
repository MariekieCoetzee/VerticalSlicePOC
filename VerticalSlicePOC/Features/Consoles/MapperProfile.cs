using AutoMapper;
using VerticalSlicePOC.Domain;

namespace VerticalSlicePOC.Features.Consoles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<GameConsole, GetAllConsoles.ConsoleResult>();
    }
}