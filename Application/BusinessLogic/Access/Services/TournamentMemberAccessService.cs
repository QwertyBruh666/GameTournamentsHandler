using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Access.Services;

public class TournamentMemberAccessService(IAppDbContext appDbContext) : BaseAccessService<TournamentMember>
{
    public override async Task<TournamentMember> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        AccessEntityDto<TournamentMember>? tm = await appDbContext.TournamentMembers
            .Where(tm => tm.Id == id)
            .Select(tm =>
                new AccessEntityDto<TournamentMember>(tm,
                    tm.Tournament!.OrganizerId == userId ||
                    appDbContext.Users.Any(u => u.Id == userId && u.UserRole == UserRole.Admin)))
            .FirstOrDefaultAsync(cancellationToken);

        return CheckAnswer(tm);
    }
}