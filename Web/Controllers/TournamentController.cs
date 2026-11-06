using Application.BusinessLogic.Matches.Commands;
using Application.BusinessLogic.Matches.Dtos;
using Application.BusinessLogic.Tournaments.Commands;
using Application.BusinessLogic.Tournaments.Criteria;
using Application.BusinessLogic.Tournaments.Dtos;
using Application.BusinessLogic.Tournaments.Queries;
using Application.BusinessLogic.TournamentsMembers.Commands;
using Application.BusinessLogic.TournamentsMembers.Dtos;
using Application.BusinessLogic.TournamentsMembers.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/tournaments")]
public class TournamentController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentDto newTournament)
    {
        return Ok(await sender.Send(new CreateTournamentCommand(newTournament)));
    }

    [HttpGet]
    public async Task<ActionResult<List<TournamentDto>>> GetTournaments([FromQuery] TournamentFilter filter)
    {
        List<TournamentDto> tournaments = await sender.Send(new GetTournamentsQuery(filter));
        return Ok(tournaments);
    }

    [Authorize]
    [HttpPut("{tournamentId}")]
    public async Task<IActionResult> UpdateTournament(Guid tournamentId, [FromBody] UpdateTournamentDto updateTournament)
    {
        await sender.Send(new UpdateTournamentCommand(tournamentId, updateTournament));
        return Ok();
    }

    [Authorize]
    [HttpPut("{tournamentId}/status")]
    public async Task<IActionResult> UpdateTournamentStatus(Guid tournamentId, [FromBody] TournamentStatus newTournamentStatus)
    {
        await sender.Send(new UpdateTournamentStatusCommand(tournamentId, newTournamentStatus));
        return Ok();
    }

    [Authorize]
    [HttpDelete("{tournamentId}")]
    public async Task<IActionResult> DeleteTournament(Guid tournamentId)
    {
        await sender.Send(new RemoveTournamentCommand(tournamentId));
        return Ok();
    }

    [Authorize]
    [HttpPost("{tournamentId}/members")]
    public async Task<ActionResult<Guid>> AddMember(Guid tournamentId, [FromBody] Guid teamId)
    {
        return Ok(await sender.Send(new AddTournamentMemberCommand(tournamentId, teamId)));
    }

    [HttpGet("{tournamentId}/members")]
    public async Task<ActionResult<List<TournamentMemberDto>>> GetTournamentMembers(Guid tournamentId)
    {
        List<TournamentMemberDto> members = await sender.Send(new GetTournamentMembersQuery(tournamentId));
        return Ok(members);
    }
    
    [Authorize]
    [HttpPost("{tournamentId}/matches")]
    public async Task<IActionResult> CreateMatch(Guid tournamentId, [FromBody] CreateMatchDto match)
    {
        return Ok(await sender.Send(new CreateMatchCommand(tournamentId, match)));
    }
}