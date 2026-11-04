namespace Domain.Entities;

public class Team : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public List<TeamMember>? Members { get; set; }
    public List<TournamentMember>? Tournaments { get; set; }
}