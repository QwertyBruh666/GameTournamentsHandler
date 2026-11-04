using Application.BusinessLogic.Games.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Games.Queries;

public record GetGamesQuery : IRequest<List<GameDto>>, ICacheable
{
    public string GetCacheKey() => "games";
    public TimeSpan CacheDuration { get; } = TimeSpan.FromMinutes(5);
}