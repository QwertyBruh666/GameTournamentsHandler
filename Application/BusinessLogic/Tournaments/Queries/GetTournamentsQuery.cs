using System.Text.Json;
using Application.BusinessLogic.Tournaments.Criteria;
using Application.BusinessLogic.Tournaments.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Tournaments.Queries;

public record GetTournamentsQuery(TournamentFilter Filter) : IRequest<List<TournamentDto>>, ICacheable
{
    public string GetCacheKey() => $"tournaments:{JsonSerializer.Serialize(Filter)}";
    public TimeSpan CacheDuration { get; } =  TimeSpan.FromSeconds(60);
}