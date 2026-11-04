using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Commands;

public record UpdateTournamentStatusCommand(Guid TournamentId, TournamentStatus NewStatus) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => $"tournaments:{TournamentId}";
}