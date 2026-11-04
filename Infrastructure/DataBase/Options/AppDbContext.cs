using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Infrastructure.DataBase.Options;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<RoleUpgradeRequest> RoleUpgradeRequests { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<AuthToken> AuthTokens { get; set; }
    public DbSet<TournamentMember> TournamentMembers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await base.SaveChangesAsync(cancellationToken);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamMember>().HasOne(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        
        modelBuilder.Entity<RoleUpgradeRequest>().HasOne(urm => urm.Requester).WithMany().HasForeignKey(urm => urm.RequesterId);
        
        modelBuilder.Entity<Game>().HasMany(g => g.Teams).WithOne(t => t.Game).HasForeignKey(t => t.GameId);
        modelBuilder.Entity<Game>().HasMany(g => g.Tournaments).WithOne(t => t.Game).HasForeignKey(t => t.GameId);
        
        modelBuilder.Entity<Team>().HasMany(t => t.Members).WithOne(t => t.Team).HasForeignKey(t => t.TeamId);
        
        modelBuilder.Entity<Match>().HasOne(m => m.Participant1).WithMany().HasForeignKey(m => m.Participant1Id);
        modelBuilder.Entity<Match>().HasOne(m => m.Participant2).WithMany().HasForeignKey(m => m.Participant2Id);
        modelBuilder.Entity<Match>().HasOne(m => m.NextMatch).WithMany().HasForeignKey(m => m.NextMatchId);
        
        modelBuilder.Entity<Tournament>().HasMany(t => t.Matches).WithOne(t => t.Tournament).HasForeignKey(t => t.TournamentId);
        modelBuilder.Entity<Tournament>().HasMany(t => t.Participants).WithOne(t => t.Tournament).HasForeignKey(t => t.TournamentId);
        modelBuilder.Entity<Tournament>().HasOne(t => t.Organizer).WithMany().HasForeignKey(t => t.OrganizerId);
        modelBuilder.Entity<Tournament>().HasOne(t => t.Winner).WithOne().HasForeignKey<Tournament>(t => t.WinnerId);
    }
}

public class AppDbContextExtensions : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseNpgsql("Host=localhost;Port=5432;Database=GameTournamentsHandlerDb;Username=admin;Password=admin;");
        return new AppDbContext(options.Options);
    }
}