using Domain.Enums;

namespace Domain.Entities;

public class TournamentMember : BaseEntity
{
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    public Guid TournamentId { get; set; }
    public Tournament? Tournament { get; set; }
    public TournamentMemberStatus Status { get; set; }
}