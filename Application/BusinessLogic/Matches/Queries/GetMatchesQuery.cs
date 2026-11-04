using System.Text.Json;
using Application.BusinessLogic.Matches.Criteria;
using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Matches.Queries;

public record GetMatchesQuery(MatchFilter Filter) : IRequest<List<MatchDto>>, ICacheable
{
    public string GetCacheKey() => $"matches:{JsonSerializer.Serialize(Filter)}";
    public TimeSpan CacheDuration { get; } = TimeSpan.FromSeconds(1);
}