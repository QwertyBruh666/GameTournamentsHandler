using Domain.Enums;

namespace Application.BusinessLogic.Matches.Dtos;

public record MatchTeamDto(Guid? TeamId, string? TeamName);
public record MatchResultDto(double? Team1Score, double? Team2Score);
public record MatchDto(
    Guid TournamentId,
    MatchStatus Status,
    DateTime? ScheduleStartsAt,
    DateTime? ScheduleEndsAt,
    DateTime? StartsAt,
    DateTime? EndsAt,
    MatchTeamDto? Team1,
    MatchTeamDto? Team2,
    MatchResultDto? MatchResult);