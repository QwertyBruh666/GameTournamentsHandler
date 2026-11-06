using Application.BusinessLogic.RoleUpgradeRequests.Dtos;
using Application.BusinessLogic.RoleUpgradeRequests.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.RoleUpgradeRequests.EventHandlers;

public class GetRoleUpgradeRequestsQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetRoleUpgradeRequestsQuery, List<RoleUpgradeRequestDto>>
{
    public async Task<List<RoleUpgradeRequestDto>> Handle(GetRoleUpgradeRequestsQuery request, CancellationToken cancellationToken)
    {
        return await appDbContext.RoleUpgradeRequests
            .Select(r => new RoleUpgradeRequestDto(r.RequesterId, r.NewRole)).ToListAsync(cancellationToken);
    }
}