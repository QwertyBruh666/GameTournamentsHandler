using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Access.Services;

public class RoleUpgradeRequestsAccessService(IAppDbContext appDbContext) : BaseAccessService<RoleUpgradeRequest>
{
    public override async Task<RoleUpgradeRequest> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var roleUpdateRequest = await appDbContext.RoleUpgradeRequests.Include(rur => rur.Requester)
            .Where(rur => rur.Id == id && rur.Status == RoleRequestStatus.Pending)
            .Select(rur => new AccessEntityDto<RoleUpgradeRequest>(rur, appDbContext.Users.Any(u => u.Id == userId && u.UserRole == UserRole.Admin)))
            .FirstOrDefaultAsync(cancellationToken);
        
        return CheckAnswer(roleUpdateRequest);
    }
}