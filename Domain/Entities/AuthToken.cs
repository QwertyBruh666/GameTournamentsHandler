namespace Domain.Entities;

public class AuthToken : BaseEntity
{
    public required Guid UserId { get; set; }
    public User? User { get; set; }
    public required string Jti { get; set; }
}