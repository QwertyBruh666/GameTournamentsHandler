using Application.BusinessLogic.TournamentsMembers.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.TournamentsMembers.EventHandlers;

public class AddTournamentMemberCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<AddTournamentMemberCommand, Guid>
{
    public async Task<Guid> Handle(AddTournamentMemberCommand request, CancellationToken cancellationToken)
    {
        var tournamentInfo = await appDbContext.Tournaments
            .Where(
                t => 
                    t.Id == request.TournamentId &&
                    (t.OrganizerId == currUser.GetUserId() || appDbContext.Users.Any(u => u.Id == currUser.GetUserId() && u.UserRole == UserRole.Organizer)))
            .Select(
                t => new { 
                    Tournament = t,
                    IsAlreadyAdded = t.Participants!.Any(p => p.TeamId == request.NewMemberTeamId),
                    IsEnoughRights = t.OrganizerId == currUser.GetUserId() || appDbContext.Users.Any(u => u.Id == currUser.GetUserId() && u.UserRole == UserRole.Admin)
                })
            .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Tournament not found");
        
        if (!tournamentInfo.IsEnoughRights)
            throw new Exception("You dont have enough rights");
        
        Tournament tournament = tournamentInfo.Tournament;
        
        if (!(DateTime.UtcNow >= tournament.RegistrationOpensAt && DateTime.UtcNow <= tournament.RegistrationClosesAt))
            throw new Exception("Tournament registration is ended");
        
        Team team = await appDbContext.Teams.FindAsync(request.NewMemberTeamId, cancellationToken) ??  throw new Exception("Team not found"); 
        
        if (tournament.GameId != team.GameId)
            throw new Exception("This team cannot take part in this tournament");
        
        if (tournamentInfo.IsAlreadyAdded)
            throw new Exception("Team already added");

        TournamentMember newMember = new TournamentMember { TournamentId = request.TournamentId, TeamId = request.NewMemberTeamId, Status = TournamentMemberStatus.Confirmed};
        await appDbContext.TournamentMembers.AddAsync(newMember, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return newMember.Id;
    }
}