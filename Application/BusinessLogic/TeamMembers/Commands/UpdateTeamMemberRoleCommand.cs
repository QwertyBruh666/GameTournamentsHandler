using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.TeamMembers.Commands;

public record UpdateTeamMemberRoleCommand(Guid TeamMemberId, TeamMemberRole NewRole) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => "team-members";
}