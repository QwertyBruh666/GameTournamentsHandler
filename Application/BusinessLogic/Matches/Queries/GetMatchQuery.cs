using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Matches.Queries;

public record GetMatchQuery : IRequest<MatchDto>, ICacheable
{
    public required Guid MatchId { get; init; }
    public TimeSpan CacheDuration { get; } = TimeSpan.FromMinutes(5);
    public string GetCacheKey() => $"matches:{MatchId}";
}