namespace Application.BusinessLogic.TournamentsMembers.Dtos;

public record TournamentMemberDto(Guid TeamId, string TeamName, Guid GameId, string GameName);