using Application.Contracts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Access.Services;

public class TournamentAccessService(IAppDbContext appDbContext) : BaseAccessService<Tournament>
{
    public override async Task<Tournament> GetAuthorizedAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        AccessEntityDto<Tournament>? tournament = await appDbContext.Tournaments.Where(t => t.Id == id)
            .Select(t => new AccessEntityDto<Tournament>(t, t.OrganizerId == userId || appDbContext.Users.Any(u => u.Id == userId && u.UserRole == UserRole.Admin)))
            .FirstOrDefaultAsync(cancellationToken);

        return CheckAnswer(tournament);
    }
}