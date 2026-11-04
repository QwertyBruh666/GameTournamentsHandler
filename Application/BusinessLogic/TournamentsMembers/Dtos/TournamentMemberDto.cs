namespace Application.BusinessLogic.TournamentsMembers.Dtos;

public record TournamentMemberDto(Guid TournamentMemberId, string TeamName, Guid GameId, string GameName);