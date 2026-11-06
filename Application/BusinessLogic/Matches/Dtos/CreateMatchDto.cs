using Domain.Enums;

namespace Application.BusinessLogic.Matches.Dtos;

public record CreateMatchDto(
    DateTime StartsAt,
    DateTime EndsAt,
    int Round,
    int Position,
    Guid? Team1Id = null,
    Guid? Team2Id = null);