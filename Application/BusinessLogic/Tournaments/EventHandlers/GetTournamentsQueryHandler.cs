using Application.BusinessLogic.Tournaments.Criteria;
using Application.BusinessLogic.Tournaments.Dtos;
using Application.BusinessLogic.Tournaments.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Tournaments.EventHandlers;

public class GetTournamentsQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetTournamentsQuery, List<TournamentDto>>
{
    public async Task<List<TournamentDto>> Handle(GetTournamentsQuery request, CancellationToken cancellationToken)
    {
        TournamentFilter filter = request.Filter;
        
        return await appDbContext.Tournaments
            .Where(t => 
                (filter.OrganizerId == null || t.OrganizerId == filter.OrganizerId) &&
                (filter.WinnerId == null || t.WinnerId == filter.WinnerId) &&
                (filter.GameId == null || t.GameId == filter.GameId) &&
                (filter.Status == null || t.Status == filter.Status) &&
                (filter.RegistrationOpensAt == null || t.RegistrationOpensAt == filter.RegistrationOpensAt) &&
                (filter.RegistrationClosesAt == null || t.RegistrationClosesAt == filter.RegistrationClosesAt) &&
                ((filter.StartsAt == null || filter.EndsAt == null) || t.StartsAt >= filter.StartsAt && t.EndsAt <= filter.EndsAt) &&
                (filter.ParticipantsCount == null || t.ParticipantsCount == filter.ParticipantsCount))
            .OrderByDescending(t => t.RegistrationOpensAt)
            .Select(t => 
                new TournamentDto(
                        t.Id,
                    t.Name, 
                        t.Description, 
                        new OrganizerDto(t.OrganizerId, t.Organizer!.UserName, t.Organizer!.Email),
                        new TournamentGameDto(t.GameId, t.Game!.Name),
                        t.WinnerId != null ? new TournamentWinnerDto(t.Winner!.Id, t.Winner!.Team!.Name) : null,
                        t.Status, 
                        t.RegistrationOpensAt, 
                        t.RegistrationClosesAt, 
                        t.StartsAt, 
                        t.EndsAt, 
                        t.ParticipantsCount))
            .ToListAsync(cancellationToken);
    }
}