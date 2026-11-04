namespace Application.BusinessLogic.Teams.Criteria;

public record TeamFilter(Guid? GameId, Guid? ClubId, int? MatchesPlayed, double? WinRate);