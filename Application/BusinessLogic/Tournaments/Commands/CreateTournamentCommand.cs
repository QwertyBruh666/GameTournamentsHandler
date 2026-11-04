using Application.BusinessLogic.Tournaments.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Commands;

public record CreateTournamentCommand(CreateTournamentDto Tournament) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => "tournaments";
}