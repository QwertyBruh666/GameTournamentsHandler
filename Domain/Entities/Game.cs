namespace Domain.Entities;

public class Game : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; }
    public List<Team>? Teams { get; set; }
    public List<Tournament>? Tournaments { get; set; }
}