using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Commands;

public record RemoveTournamentCommand(Guid TournamentId) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => "tournaments";
}