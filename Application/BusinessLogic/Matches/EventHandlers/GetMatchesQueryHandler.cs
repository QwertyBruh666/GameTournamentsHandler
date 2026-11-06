using Application.BusinessLogic.Matches.Criteria;
using Application.BusinessLogic.Matches.Dtos;
using Application.BusinessLogic.Matches.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Matches.EventHandlers;

public class GetMatchesQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetMatchesQuery, List<MatchDto>>
{
    public async Task<List<MatchDto>> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
    {
        MatchFilter filter = request.Filter;
        return await appDbContext.Matches
            .Where(m =>
                (filter.TournamentId == null || m.TournamentId == filter.TournamentId) &&
                (filter.Round == null || m.Round == filter.Round) &&
                (filter.TeamId == null || (m.Participant1!.TeamId == filter.TeamId || m.Participant2!.TeamId == filter.TeamId)) && 
                ((filter.Team1Id == null || filter.Team2Id == null) || m.Participant1Id ==  filter.Team1Id && m.Participant2Id == filter.Team2Id) &&
                (filter.Status == null || m.Status == filter.Status) &&
                (filter.FromDate == null || m.ScheduleStartsAt >= filter.FromDate) &&
                (filter.ToDate == null || m.ScheduleEndsAt <= filter.ToDate))
            .OrderByDescending(m => m.ScheduleStartsAt)
            .Select(m => new MatchDto(
                    m.TournamentId, 
                    m.Status,
                    m.ScheduleStartsAt, 
                    m.ScheduleStartsAt, 
                    m.ActualStartsAt,
                    m.ActualEndsAt,
                    m.Participant1Id != null ? new MatchTeamDto(m.Participant1Id, m.Participant1!.Team!.Name) : null, 
                    m.Participant2Id != null ? new MatchTeamDto(m.Participant2Id, m.Participant2!.Team!.Name) : null, 
                    new MatchResultDto(m.Participant1Score, m.Participant2Score)
                ))
            .ToListAsync(cancellationToken);
    }
}