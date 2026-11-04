namespace Application.BusinessLogic.Teams.Dtos;

public record CreateTeamDto(string TeamName, string Description, Guid GameId);