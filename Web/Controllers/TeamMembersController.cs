using Application.BusinessLogic.TeamMembers.Commands;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/team-members")]
public class TeamMembersController(ISender sender) : ControllerBase
{
    [HttpPut("{memberId}")]
    public async Task<IActionResult> UpdateTeamMemberRole(Guid memberId, [FromBody] TeamMemberRole role)
    {
        await sender.Send(new UpdateTeamMemberRoleCommand(memberId, role));
        return Ok();
    }

    [HttpDelete("{memberId}")]
    public async Task<IActionResult> DeleteTeamMember(Guid memberId)
    {
        await sender.Send(new RemoveTeamMemberCommand(memberId));
        return Ok();
    }
}