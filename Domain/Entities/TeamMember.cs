using Domain.Enums;

namespace Domain.Entities;

public class TeamMember : BaseEntity
{
    public TeamMemberRole Role { get; set; }
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}