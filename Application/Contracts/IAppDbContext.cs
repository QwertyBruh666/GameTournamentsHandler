using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts;

public interface IAppDbContext
{
    DbSet<AuthToken> AuthTokens { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<RoleUpgradeRequest> RoleUpgradeRequests { get; set; }
    DbSet<Game> Games { get; set; }
    DbSet<Match> Matches { get; set; }
    DbSet<Team> Teams { get; set; }
    DbSet<TeamMember> TeamMembers { get; set; }
    DbSet<Tournament> Tournaments { get; set; }
    DbSet<TournamentMember> TournamentMembers { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}