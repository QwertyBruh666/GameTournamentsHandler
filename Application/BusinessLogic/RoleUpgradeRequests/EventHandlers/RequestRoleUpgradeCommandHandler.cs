using Application.BusinessLogic.RoleUpgradeRequests.Commands;
using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.RoleUpgradeRequests.EventHandlers;

public class RequestRoleUpgradeCommandHandler(IAppDbContext appDbContext, ICurrentUser currUser) : IRequestHandler<RequestRoleUpgradeCommand>
{
    public async Task Handle(RequestRoleUpgradeCommand request, CancellationToken cancellationToken)
    {
        User admin = await appDbContext.Users.FirstAsync(u => u.UserRole == UserRole.Admin, cancellationToken);
        await appDbContext.RoleUpgradeRequests.AddAsync(new RoleUpgradeRequest { RequesterId = currUser.GetUserId(), NewRole = request.NewRole}, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}