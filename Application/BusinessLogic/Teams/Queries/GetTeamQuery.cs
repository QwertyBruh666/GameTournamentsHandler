using Application.BusinessLogic.Teams.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Teams.Queries;

public record GetTeamQuery(Guid TeamId) : IRequest<TeamDto>, ICacheable
{
    public string GetCacheKey() => $"teams:{TeamId}";
    public TimeSpan CacheDuration { get; } = TimeSpan.FromHours(5);
}