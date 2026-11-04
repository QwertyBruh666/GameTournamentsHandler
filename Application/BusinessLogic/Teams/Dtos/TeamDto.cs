namespace Application.BusinessLogic.Teams.Dtos;

public record TeamDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int MembersCount { get; init; }
    public int Loses { get; init; }
    public double WinRate { get; init; }

    public TeamDto(Guid id, string name, string description, int memberCount, int matchPlayed, int wins)
    {
        Id = id;
        Name = name;
        Description = description;
        MembersCount = memberCount;
        Loses = matchPlayed -  wins;
        if(matchPlayed == 0)
            WinRate = 0;
        else
            WinRate = (double)wins / matchPlayed;
    }
}