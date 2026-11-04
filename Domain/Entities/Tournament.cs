using Domain.Enums;

namespace Domain.Entities;

public class Tournament : BaseEntity
{
    public Guid OrganizerId { get; set; }
    public User? Organizer { get; set; }
    public Guid? WinnerId { get; set; }
    public Team? Winner { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public TournamentStatus Status { get; set; } = TournamentStatus.Pending;
    public DateTime RegistrationOpensAt { get; set; }
    public DateTime RegistrationClosesAt { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public List<TournamentMember>? Participants { get; set; }
    public int ParticipantsCount { get; set; }
    public List<Match>? Matches  { get; set; }
}