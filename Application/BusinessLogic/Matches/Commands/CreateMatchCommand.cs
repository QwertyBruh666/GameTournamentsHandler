using Application.BusinessLogic.Matches.Dtos;
using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.Matches.Commands;

public record CreateMatchCommand(Guid TournamentId, CreateMatchDto Match) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => "matches";
}