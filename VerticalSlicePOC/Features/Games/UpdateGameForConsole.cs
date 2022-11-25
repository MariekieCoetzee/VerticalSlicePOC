using AutoMapper;
using MediatR;
using VerticalSlicePOC.Features.Games.Exceptions;
using VerticalSlicePOC.ServiceManager;

namespace VerticalSlicePOC.Features.Games;

public class UpdateGameForConsole
{
    public class UpdateGameCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public int ConsoleId { get; set; }
    }

    public class UpdateGameResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public int ConsoleId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateGameCommand>
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public Handler(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var console = await _serviceManager.Console.GetConsoleByIdAsync(request.ConsoleId);
            if (console is null)
            {
                throw new NoConsoleExistsException(request.ConsoleId);
            }

            var game = await _serviceManager.Game.GetGameAsync(request.ConsoleId, request.Id);

            if (game is null)
            {
                throw new NoGameExistsException(request.ConsoleId, request.Id);
            }

            game.Name = request.Name;
            game.Publisher = request.Publisher;

            await _serviceManager.SaveAsync();
            return Unit.Value;
        }
    }
}