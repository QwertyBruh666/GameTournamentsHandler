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
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/tournaments")]
public class TournamentController(ISender sender) : ControllerBase
{
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

    [HttpPut("{tournamentId}")]
    public async Task<IActionResult> UpdateTournament(Guid tournamentId, [FromBody] UpdateTournamentDto updateTournament)
    {
        await sender.Send(new UpdateTournamentCommand(tournamentId, updateTournament));
        return Ok();
    }

    [HttpPut("{tournamentId}/status")]
    public async Task<IActionResult> UpdateTournamentStatus(Guid tournamentId, [FromBody] TournamentStatus newTournamentStatus)
    {
        await sender.Send(new UpdateTournamentStatusCommand(tournamentId, newTournamentStatus));
        return Ok();
    }

    [HttpDelete("{tournamentId}")]
    public async Task<IActionResult> DeleteTournament(Guid tournamentId)
    {
        await sender.Send(new RemoveTournamentCommand(tournamentId));
        return Ok();
    }

    [HttpPost("{tournamentId}/members")]
    public async Task<ActionResult<Guid>> AddMember(Guid tournamentId, [FromBody] Guid teamId)
    {
        Guid tournamentMemberId = await sender.Send(new AddTournamentMemberCommand(tournamentId, teamId));
        return Ok(tournamentMemberId);
    }

    [HttpGet("{tournamentId}/members")]
    public async Task<ActionResult<List<TournamentMemberDto>>> GetTournamentMembers(Guid tournamentId)
    {
        List<TournamentMemberDto> members = await sender.Send(new GetTournamentMembersQuery(tournamentId));
        return Ok(members);
    }
    
    [HttpPost("{tournamentId}/matches")]
    public async Task<IActionResult> CreateMatch(Guid tournamentId, CreateMatchDto match)
    {
        await sender.Send(new CreateMatchCommand(tournamentId, match));
        return Ok();
    }
}