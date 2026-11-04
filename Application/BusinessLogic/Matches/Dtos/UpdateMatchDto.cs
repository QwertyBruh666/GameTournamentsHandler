using Domain.Enums;

namespace Application.BusinessLogic.Matches.Dtos;

public record UpdateMatchDto(
    Guid? Team1Id = null,
    Guid? Team2Id = null,
    Guid? TournamentId = null,
    MatchStatus? Status = null,
    DateTime? StartsAt = null,
    DateTime? EndsAt = null,
    int? Round = null,
    int? Position = null,
    double? Team1Score = null,
    double? Team2Score = null);