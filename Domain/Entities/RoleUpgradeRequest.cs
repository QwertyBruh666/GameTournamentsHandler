using Domain.Enums;

namespace Domain.Entities;

public class RoleUpgradeRequest : BaseEntity
{
    public UserRole NewRole { get; set; }
    public Guid RequesterId { get; set; }
    public User? Requester { get; set; }
    public RoleRequestStatus Status { get; set; } = RoleRequestStatus.Pending;
}