namespace Application.BusinessLogic.Teams.Dtos;

public record UpdateTeamDto(Guid? GameId, Guid? ClubId, string? Name);