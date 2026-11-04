using Application.BusinessLogic.TeamMembers.Commands;
using Application.BusinessLogic.TeamMembers.Dtos;
using Application.BusinessLogic.TeamMembers.Queries;
using Application.BusinessLogic.Teams.Commands;
using Application.BusinessLogic.Teams.Criteria;
using Application.BusinessLogic.Teams.Dtos;
using Application.BusinessLogic.Teams.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/teams")]
public class TeamController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody]CreateTeamDto newTeam)
    {
        await sender.Send(new AddTeamCommand(newTeam));
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<TeamDto>>> GetTeams([FromQuery] TeamFilter filter)
    {
        List<TeamDto> teams = await sender.Send(new GetTeamsQuery(filter));
        return Ok(teams);
    }

    [HttpGet("{teamId}")]
    public async Task<ActionResult<TeamDto>> GetTeam(Guid teamId)
    {
        TeamDto team = await sender.Send(new GetTeamQuery(teamId));
        return Ok(team);
    }

    [HttpPut("{teamId}")]
    public async Task<ActionResult<Guid>> UpdateTeam(Guid teamId, [FromBody] UpdateTeamDto team)
    {
        await sender.Send(new UpdateTeamCommand(teamId, team));
        return Ok();
    }

    [HttpDelete("{teamId}")]
    public async Task<IActionResult> DeleteTeam(Guid teamId)
    {
        await sender.Send(new RemoveTeamCommand(teamId));
        return Ok();
    }

    [HttpPost("{teamId}/members")]
    public async Task<ActionResult<Guid>> AddTeamMember(Guid teamId, [FromBody] CreateTeamMemberDto team)
    {
       Guid memberId = await sender.Send(new AddTeamMemberCommand(teamId, team));
       return Ok(memberId);
    }

    [HttpGet("{teamId}/members")]
    public async Task<ActionResult<List<TeamMemberDto>>> GetTeamMembers(Guid teamId)
    {
        List<TeamMemberDto> members = await sender.Send(new GetTeamMembersQuery(teamId));
        return Ok(members);
    }
} 