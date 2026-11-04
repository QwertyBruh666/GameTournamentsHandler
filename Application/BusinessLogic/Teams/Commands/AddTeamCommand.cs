using Application.BusinessLogic.Teams.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Teams.Commands;

public record AddTeamCommand(CreateTeamDto Team) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => "teams";
}