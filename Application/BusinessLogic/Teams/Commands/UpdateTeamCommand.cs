using Application.BusinessLogic.Teams.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Teams.Commands;

public record UpdateTeamCommand(Guid TeamId, UpdateTeamDto Team) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => $"teams:{TeamId}";
}