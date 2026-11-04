using Application.BusinessLogic.Tournaments.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Commands;

public record UpdateTournamentCommand(Guid TournamentId,  UpdateTournamentDto Tournament) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => $"tournaments:{TournamentId}";
}