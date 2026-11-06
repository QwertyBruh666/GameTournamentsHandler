namespace Application.BusinessLogic.TournamentsMembers.Dtos;

public record TournamentMemberDto(Guid TournamentMemberId, Guid TeamId, string TeamName, Guid GameId, string GameName);