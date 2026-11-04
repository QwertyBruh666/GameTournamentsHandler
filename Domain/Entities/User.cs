using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
    public required string UserName  { get; set; }
    public required string Email  { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole UserRole { get; set; } =  UserRole.Viewer;
    public List<Tournament>? OrganizedTournaments { get; set; }
    public List<TeamMember>? Memberships { get; set; }
}