namespace Application.BusinessLogic.Tournaments.Dtos;

public record CreateTournamentDto(
    string Name,
    string Description,
    Guid GameId,
    DateTime RegistrationOpensAt,
    DateTime RegistrationClosesAt,
    DateTime StartsAt,
    DateTime EndsAt,
    int ParticipantsCount
);