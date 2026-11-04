using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Matches.Commands;

public record StartMatchCommand(Guid MatchId) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => $"matches:{MatchId}";
}