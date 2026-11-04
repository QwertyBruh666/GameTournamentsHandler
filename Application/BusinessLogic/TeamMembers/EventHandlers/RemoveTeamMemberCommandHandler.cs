using Application.BusinessLogic.TeamMembers.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.TeamMembers.EventHandlers;

public class RemoveTeamMemberCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<TeamMember> auth) : IRequestHandler<RemoveTeamMemberCommand>
{
    public async Task Handle(RemoveTeamMemberCommand request, CancellationToken cancellationToken)
    {
        TeamMember teamMember = await auth.GetAuthorizedAsync(request.TeamMemberId, currUser.GetUserId(), cancellationToken);
        appDbContext.TeamMembers.Remove(teamMember);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}