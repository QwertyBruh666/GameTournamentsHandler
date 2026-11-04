using System.Text.Json;
using Application.BusinessLogic.Teams.Criteria;
using Application.BusinessLogic.Teams.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Teams.Queries;

public record GetTeamsQuery(TeamFilter Filter) : IRequest<List<TeamDto>>, ICacheable
{
    public string GetCacheKey() => $"teams:{JsonSerializer.Serialize(Filter)}";
    public TimeSpan CacheDuration { get; } =  TimeSpan.FromSeconds(60);
}