using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Access.Services;

public class TeamAccessService(IAppDbContext appDbContext) : BaseAccessService<Team>
{
    public override async Task<Team> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var team = await appDbContext.Teams
            .Where(t => t.Id == id)
            .Select(t => new AccessEntityDto<Team>(t, (t.Members!.Any(m => m.UserId == userId && m.Role == TeamMemberRole.Capitan) || 
                                                 appDbContext.Users.Any(u => u.Id == userId && u.UserRole == UserRole.Admin))))
            .FirstOrDefaultAsync(cancellationToken);
        
        return CheckAnswer(team);
    }
}