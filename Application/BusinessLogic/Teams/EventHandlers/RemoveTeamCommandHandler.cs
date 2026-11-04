using Application.BusinessLogic.Teams.Commands;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.BusinessLogic.Teams.EventHandlers;

public class RemoveTeamCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Team> auth) : IRequestHandler<RemoveTeamCommand>
{
    public async Task Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
    {
        Team team = await auth.GetAuthorizedAsync(request.TeamId, currUser.GetUserId(), cancellationToken);
        
        appDbContext.Teams.Remove(team);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}