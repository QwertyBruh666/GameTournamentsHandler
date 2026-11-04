using Domain.Enums;

namespace Application.BusinessLogic.Matches.Dtos;

public record CreateMatchDto(
    DateTime StartsAt,
    DateTime EndsAt,
    MatchStatus Status = MatchStatus.Pending,
    Guid? Team1Id = null,
    Guid? Team2Id = null);