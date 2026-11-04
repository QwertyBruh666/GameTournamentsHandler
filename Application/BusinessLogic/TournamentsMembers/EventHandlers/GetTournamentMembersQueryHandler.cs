using Application.BusinessLogic.TournamentsMembers.Dtos;
using Application.BusinessLogic.TournamentsMembers.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.TournamentsMembers.EventHandlers;

public class GetTournamentMembersQueryHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<GetTournamentMembersQuery, List<TournamentMemberDto>>
{
    public async Task<List<TournamentMemberDto>> Handle(GetTournamentMembersQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.TournamentMembers
            .Where(tm => tm.TournamentId == request.TournamentId)
            .Select(tm => new TournamentMemberDto(tm.Id, tm.Team!.Name, tm.Team!.GameId, tm.Team!.Game.Name))
            .ToListAsync(cancellationToken);
    }
}