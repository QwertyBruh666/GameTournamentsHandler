using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Matches.Commands;

public record StartMatchCommand(Guid MatchId) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => $"matches:{MatchId}";
}