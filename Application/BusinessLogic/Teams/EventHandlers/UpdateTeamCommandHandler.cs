using Application.BusinessLogic.Teams.Commands;
using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.BusinessLogic.Teams.EventHandlers;

public class UpdateTeamCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<Team> auth) : IRequestHandler<UpdateTeamCommand>
{
    public async Task Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        Team team = await auth.GetAuthorizedAsync(request.TeamId, currUser.GetUserId(), cancellationToken);
        team.Name = request.Team.Name ?? team.Name;
        team.GameId = request.Team.GameId ?? team.GameId;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}