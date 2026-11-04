using Application.BusinessLogic.RoleUpgradeRequests.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.RoleUpgradeRequests.EventHandlers;

public class RejectRoleUpgradeCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<RoleUpgradeRequest> auth) : IRequestHandler<RejectRoleUpgradeCommand>
{
    public async Task Handle(RejectRoleUpgradeCommand request, CancellationToken cancellationToken)
    {
        RoleUpgradeRequest roleUpdateRequest = await auth.GetAuthorizedAsync(request.RequestId, currUser.GetUserId(), cancellationToken);
        roleUpdateRequest.Status = RoleRequestStatus.Reject;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}