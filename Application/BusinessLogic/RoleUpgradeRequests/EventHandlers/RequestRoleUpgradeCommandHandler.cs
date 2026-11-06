using Application.BusinessLogic.RoleUpgradeRequests.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.RoleUpgradeRequests.EventHandlers;

public class RequestRoleUpgradeCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<RequestRoleUpgradeCommand, Guid>
{
    public async Task<Guid> Handle(RequestRoleUpgradeCommand request, CancellationToken cancellationToken)
    {
        RoleUpgradeRequest requestDb = new RoleUpgradeRequest
            { RequesterId = currUser.GetUserId(), NewRole = request.NewRole };
        await appDbContext.RoleUpgradeRequests.AddAsync(requestDb, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return requestDb.RequesterId;
    }
}