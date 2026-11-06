using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Commands;

public record UpdateTournamentStatusCommand(Guid TournamentId, TournamentStatus NewStatus) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => $"tournaments:{TournamentId}";
}