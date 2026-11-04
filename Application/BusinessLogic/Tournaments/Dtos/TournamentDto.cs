using Domain.Enums;

namespace Application.BusinessLogic.Tournaments.Dtos;

public record OrganizerDto(Guid OrganizerId, string Name, string Email);

public record TournamentGameDto(Guid GameId, string Name);

public record TournamentWinnerDto(Guid Id, string Name);

public record TournamentDto(
            Guid Id,
            string Title,
            string Description,
            OrganizerDto Organizer,
            TournamentGameDto Game,
            TournamentWinnerDto? Winner,
            TournamentStatus Status,
            DateTime RegistrationOpensAt,
            DateTime RegistrationClosesAt,
            DateTime StartsAt,
            DateTime EndsAt,
            int ParticipantsCount
        );