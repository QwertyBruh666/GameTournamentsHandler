using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Access.Services;

public class MatchAccessService(IAppDbContext appDbContext) :  BaseAccessService<Match>, IAccessService<Match>
{
    public override async Task<Match> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var matchInfo = await appDbContext.Matches.Include(m => m.Participant1).Include(m => m.Participant2)
            .Where(m =>
                m.Id == id)
            .Select(m => new AccessEntityDto<Match>(m, m.Tournament!.OrganizerId == userId || appDbContext.Users.Any(u => u.Id == userId && u.UserRole == UserRole.Admin)))
            .FirstOrDefaultAsync(cancellationToken);
        
        return CheckAnswer(matchInfo);
    }
}