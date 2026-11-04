using Application.BusinessLogic.Matches.Dtos;
using Application.BusinessLogic.Matches.Queries;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class GetMatchQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetMatchQuery, MatchDto>
{
    public async Task<MatchDto> Handle(GetMatchQuery request, CancellationToken cancellationToken)
    {
        Match? m = await appDbContext.Matches.FindAsync(request.MatchId, cancellationToken);
        if (m == null)
            throw new Exception("Match not found");

        return new MatchDto(
                    m.TournamentId, 
                    m.Status,
                    m.ScheduleStartsAt, 
                    m.ScheduleStartsAt, 
                    m.ActualStartsAt,
                    m.ActualEndsAt,
                    m.Participant1Id != null ? new MatchTeamDto(m.Participant1!.TeamId, m.Participant1!.Team!.Name) : null, 
                    m.Participant2Id != null ? new MatchTeamDto(m.Participant2!.TeamId, m.Participant2!.Team!.Name) : null, 
                    new MatchResultDto(m.Participant1Score, m.Participant2Score));
    }
}