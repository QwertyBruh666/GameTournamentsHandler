using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Games.Commands;

public record UpdateActiveStatusCommand(Guid GameId, bool IsActive) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => $"games:{GameId}";
}