using Application.BusinessLogic.TeamMembers.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.TeamMembers.EventHandlers;

public class UpdateTeamMemberRoleCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<TeamMember> auth) : IRequestHandler<UpdateTeamMemberRoleCommand, Guid>
{
    public async Task<Guid> Handle(UpdateTeamMemberRoleCommand request, CancellationToken cancellationToken)
    {
        TeamMember member = await auth.GetAuthorizedAsync(request.TeamMemberId, currUser.GetUserId(), cancellationToken);
        member.Role = request.NewRole;
        await appDbContext.SaveChangesAsync(cancellationToken);
        return member.Id;
    }
}