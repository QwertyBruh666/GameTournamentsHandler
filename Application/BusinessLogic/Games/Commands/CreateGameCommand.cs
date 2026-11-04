using Application.BusinessLogic.Games.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Games.Commands;

public record CreateGameCommand(CreateGameDto Game) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => "games";
}