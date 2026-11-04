using Application.BusinessLogic.Games.Dtos;
using Application.BusinessLogic.Games.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Games.EventHandlers;

public class GetGamesQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetGamesQuery, List<GameDto>>
{
    public async Task<List<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.Games
            .Select(g => new GameDto(g.Id, g.Name, g.Description, g.IsActive))
            .ToListAsync(cancellationToken);
    }
}