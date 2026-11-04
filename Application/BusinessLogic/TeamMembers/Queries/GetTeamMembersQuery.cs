using Application.BusinessLogic.TeamMembers.Dtos;
using Application.Contracts;
using MediatR;

namespace Application.BusinessLogic.TeamMembers.Queries;

public record GetTeamMembersQuery(Guid TeamId) : IRequest<List<TeamMemberDto>>, ICacheable
{
    public string GetCacheKey() => "team-members";
    public TimeSpan CacheDuration { get; } = TimeSpan.FromMinutes(5);
}