using Application.BusinessLogic.Teams.Criteria;
using Application.BusinessLogic.Teams.Dtos;
using Application.BusinessLogic.Teams.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Teams.EventHandlers;

public class GetTeamsQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetTeamsQuery, List<TeamDto>>
{
    public async Task<List<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        TeamFilter filter = request.Filter;
        
        return await appDbContext.Teams
            .Where(t => 
                (filter.GameId == null || t.GameId == filter.GameId) &&
                (filter.MatchesPlayed == null || appDbContext.Matches.Count(m => m.Participant1!.TeamId ==  t.Id && m.Participant2!.TeamId == t.Id) >= filter.MatchesPlayed) &&
                (filter.WinRate == null || (appDbContext.Matches.Count(m => m.Participant1!.TeamId == t.Id && m.Participant1Score > m.Participant2Score ||
                                                                            m.Participant2!.TeamId == t.Id && m.Participant2Score > m.Participant1Score) >= filter.WinRate)))
            .Select(t => new TeamDto(t.Id, 
                t.Name, 
                t.Description, 
                t.Members!.Count, 
                appDbContext.Matches.Count(m => m.Participant1!.TeamId ==  t.Id && m.Participant2!.TeamId == t.Id), 
                appDbContext.Matches.Count(m => m.Participant1!.TeamId == t.Id && m.Participant1Score > m.Participant2Score ||
                                                m.Participant2!.TeamId == t.Id && m.Participant2Score > m.Participant1Score)))
            .ToListAsync(cancellationToken);
    }
}