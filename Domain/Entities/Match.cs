using Domain.Enums;

namespace Domain.Entities;

public class Match : BaseEntity
{
    public Guid TournamentId { get; set; }
    public Guid? NextMatchId { get; set; }
    public Match? NextMatch { get; set; }
    public Tournament? Tournament { get; set; }
    public Guid? Participant1Id { get; set; }
    public Guid? Participant2Id { get; set; }
    public TournamentMember? Participant1 { get; set; }
    public TournamentMember? Participant2 { get; set; }
    public MatchStatus Status { get; set; } = MatchStatus.Pending;
    public DateTime? ScheduleStartsAt { get; set; }
    public DateTime? ScheduleEndsAt { get; set; }
    public DateTime? ActualStartsAt { get; set; }
    public DateTime? ActualEndsAt { get; set; }
    public int? Round  { get; set; }
    public int? Position { get; set; }
    public double? Participant1Score { get; set; }
    public double? Participant2Score { get; set; }
}