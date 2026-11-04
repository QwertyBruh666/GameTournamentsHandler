using Domain.Enums;

namespace Application.BusinessLogic.Tournaments.Criteria;

public record TournamentFilter(Guid? OrganizerId, Guid? WinnerId, Guid? GameId, TournamentStatus? Status, DateTime? RegistrationOpensAt, DateTime? RegistrationClosesAt, DateTime? StartsAt, DateTime? EndsAt, int? ParticipantsCount);