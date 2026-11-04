using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Access.Services;

public class TeamMemberAccessService(IAppDbContext appDbContext) : BaseAccessService<TeamMember>
{
    public override async Task<TeamMember> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var member = await appDbContext.TeamMembers
            .Where(tm => tm.Id == id)
            .Select(tm => new AccessEntityDto<TeamMember>(tm, (appDbContext.TeamMembers.Any(tmi => tmi.UserId == userId && tmi.Role == TeamMemberRole.Capitan) || appDbContext.Users.Any(u => u.Id == userId && u.UserRole == UserRole.Admin))))
            .FirstOrDefaultAsync(cancellationToken);
        
        return CheckAnswer(member);
    }
}