using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.TeamMembers.Commands;

public record RemoveTeamMemberCommand(Guid TeamMemberId) : IRequest, ICacheInvalidator
{
    public string GetCacheKey() => "team-members";
}