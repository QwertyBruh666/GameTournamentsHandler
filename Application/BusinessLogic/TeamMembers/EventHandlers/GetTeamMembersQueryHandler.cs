using Application.BusinessLogic.TeamMembers.Dtos;
using Application.BusinessLogic.TeamMembers.Queries;
using Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.TeamMembers.EventHandlers;

public class GetTeamMembersQueryHandler(IAppDbContext appDbContext) : IRequestHandler<GetTeamMembersQuery, List<TeamMemberDto>>
{
    public async Task<List<TeamMemberDto>> Handle(GetTeamMembersQuery request, CancellationToken cancellationToken)
    {
        List<TeamMemberDto> members = await appDbContext.TeamMembers.Where(t => t.TeamId == request.TeamId)
            .Select(t => new TeamMemberDto(t.TeamId, t.Team!.Name, t.User!.UserName, t.User!.Email)).ToListAsync(cancellationToken);

        return members;
    }
}