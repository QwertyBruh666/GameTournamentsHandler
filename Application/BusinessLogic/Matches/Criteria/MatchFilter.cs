using Domain.Enums;

namespace Application.BusinessLogic.Matches.Criteria;

public record MatchFilter(Guid? TournamentId, int? Round, Guid? TeamId, Guid? Team1Id, Guid? Team2Id , MatchStatus? Status, DateTime? FromDate, DateTime? ToDate);