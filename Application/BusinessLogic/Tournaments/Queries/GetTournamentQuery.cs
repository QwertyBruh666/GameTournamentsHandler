using Application.BusinessLogic.Tournaments.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Queries;

public record GetTournamentQuery(Guid Id) : IRequest<TournamentDto>, ICacheable
{
    public string GetCacheKey() => $"tournaments:{Id}";
    public TimeSpan CacheDuration { get; } = TimeSpan.FromMinutes(5);
}