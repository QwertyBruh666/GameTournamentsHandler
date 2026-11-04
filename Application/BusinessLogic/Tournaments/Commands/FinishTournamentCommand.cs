using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Commands;

public record FinishTournamentCommand(Guid TournamentId) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => $"tournaments:{TournamentId}";
}