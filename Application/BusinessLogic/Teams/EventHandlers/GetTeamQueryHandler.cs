using Application.BusinessLogic.Teams.Dtos;
using Application.BusinessLogic.Teams.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Teams.EventHandlers;

public class GetTeamQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetTeamQuery, TeamDto>
{
    public async Task<TeamDto> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        TeamDto? team = await appDbContext.Teams.Where(t => t.Id == request.TeamId)
            .Select(t => 
                new TeamDto(
                    t.Id, 
                    t.Name, 
                    t.Description, 
                    t.Members!.Count, 
                    appDbContext.Matches.Count(m => m.Participant1!.TeamId ==  t.Id && m.Participant2!.TeamId == t.Id), 
                    appDbContext.Matches.Count(m => m.Participant1!.TeamId == t.Id && m.Participant1Score > m.Participant2Score ||
                                                    m.Participant2!.TeamId == t.Id && m.Participant2Score > m.Participant1Score)))
            .FirstOrDefaultAsync(cancellationToken);

        if (team == null)
            throw new Exception("Team not found");
        
        return team;
    }
}