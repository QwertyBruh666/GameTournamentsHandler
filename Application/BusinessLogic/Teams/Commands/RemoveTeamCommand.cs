using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.Teams.Commands;

public record RemoveTeamCommand(Guid TeamId) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => "teams";
}