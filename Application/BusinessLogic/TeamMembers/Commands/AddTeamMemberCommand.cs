using Application.BusinessLogic.TeamMembers.Dtos;
using Application.BusinessLogic.Teams.Dtos;
using Application.Contracts;
using Domain.Enums;
using MediatR;

namespace Application.BusinessLogic.TeamMembers.Commands;

public record AddTeamMemberCommand(Guid TeamId, CreateTeamMemberDto Member) : IRequest<Guid>, ICacheInvalidator
{
    public string GetCacheKey() => "team-members";
}