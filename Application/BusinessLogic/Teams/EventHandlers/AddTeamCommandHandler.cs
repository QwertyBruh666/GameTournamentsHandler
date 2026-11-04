using Application.BusinessLogic.Teams.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Teams.EventHandlers;

public class AddTeamCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<AddTeamCommand, Guid>
{
    public async Task<Guid> Handle(AddTeamCommand request, CancellationToken cancellationToken)
    {
        if (!await appDbContext.Users.AnyAsync(u => u.Id == currUser.GetUserId() && u.UserRole >= UserRole.Player,
                cancellationToken))
            throw new Exception("You dont have enough rights");

        Team newTeam = new Team { Name = request.Team.TeamName, Description = request.Team.Description, GameId = request.Team.GameId };
        newTeam.Members = [new TeamMember { TeamId = newTeam.Id, UserId = currUser.GetUserId(), Role = TeamMemberRole.Capitan }];
        await appDbContext.Teams.AddAsync(newTeam, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return newTeam.Id;
    }
}