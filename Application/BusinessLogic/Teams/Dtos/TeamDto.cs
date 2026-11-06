using Domain.Enums;

namespace Application.BusinessLogic.Teams.Dtos;

public record TeamDto(Guid Id, string Name, string Description, int MembersCount, int MatchPlayed, double WinRate, int TournamentPlayed);