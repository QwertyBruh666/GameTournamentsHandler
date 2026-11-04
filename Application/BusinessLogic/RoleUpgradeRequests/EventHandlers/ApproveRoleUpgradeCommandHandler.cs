using Application.BusinessLogic.RoleUpgradeRequests.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.RoleUpgradeRequests.EventHandlers;

public class ApproveRoleUpgradeCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser, IAccessService<RoleUpgradeRequest> auth) : IRequestHandler<ApproveRoleUpgradeCommand>
{
    public async Task Handle(ApproveRoleUpgradeCommand request, CancellationToken cancellationToken)
    {
        RoleUpgradeRequest roleUpdateRequest = await auth.GetAuthorizedAsync(request.RequestId, currUser.GetUserId(), cancellationToken);
        roleUpdateRequest.Status = RoleRequestStatus.Approve;
        roleUpdateRequest.Requester!.UserRole = roleUpdateRequest.NewRole;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}