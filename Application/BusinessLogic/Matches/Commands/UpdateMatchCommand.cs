using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.Matches.Commands;

public record UpdateMatchCommand(Guid MatchId, UpdateMatchDto Match) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => $"matches:{MatchId}";
}