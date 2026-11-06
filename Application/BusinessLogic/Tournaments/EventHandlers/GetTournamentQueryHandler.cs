using Application.BusinessLogic.Tournaments.Dtos;
using Application.BusinessLogic.Tournaments.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Tournaments.EventHandlers;

public class GetTournamentQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetTournamentQuery, TournamentDto>
{
    public async Task<TournamentDto> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
    {
        TournamentDto? tournament = await appDbContext.Tournaments.Where(t => t.Id == request.Id)
            .Select(t => new TournamentDto(
                t.Id,
                t.Name,
                t.Description,
                new OrganizerDto(t.OrganizerId, t.Organizer!.UserName, t.Organizer!.Email),
                new TournamentGameDto(t.GameId, t.Game!.Name),
                t.Winner != null ? new TournamentWinnerDto(t.WinnerId!.Value, t.Winner!.Team!.Name) : null,
                t.Status,
                t.RegistrationOpensAt,
                t.RegistrationClosesAt,
                t.StartsAt,
                t.EndsAt,
                t.Participants!.Count))
            .FirstOrDefaultAsync(cancellationToken);
        
        if(tournament == null)
            throw new Exception("Tournament not found");
        
        return tournament;

    }
}