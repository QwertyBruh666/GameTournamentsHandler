using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Matches.Commands;

public record FinishMatchCommand(Guid MatchId, MatchResultRequestDto MatchResult) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => $"matches:{MatchId}";
}